set-psdebug -off
set-executionpolicy Unrestricted

function WriteImportantMessage($message)
{
	$desktopFolderName = [System.Enum]::GetValues([System.Environment+SpecialFolder])[0]
	$savePath = join-path (join-path $home $desktopFolderName) "Readme.txt"
	$message >> $savePath
}

function Get-LogPath
{
	trap [Exception]{
		return ".\HostingLog-$(get-date -format MMddyyHHmmss).log"
	}	
	$Invocation = (Get-Variable MyInvocation -Scope 1).Value
	$scriptDirectory = Split-Path $Invocation.ScriptName
    $logPath = Join-Path $scriptDirectory ".\HostingLog-$(get-date -format MMddyyHHmmss).log"
	return $logPath
}

# this function does logging
function write-log([string]$type, [string]$info){
    if($global:loginitialized -eq $false){
        $global:logfile = (Get-LogPath)
		$global:loginitialized = $true
    }
	$currentTime = get-date -format HH:mm:ss
    $logMessage = $currentTime + "`t" + $type + "`t" +  $info
	$logMessage >> $logfile
	Switch($type){
		"Error"{
			write-host -foregroundcolor white -backgroundcolor red $logMessage
		}
		"Important"{
			write-host -foregroundcolor black -backgroundcolor yellow $logMessage
		}
		default{
			write-host -foregroundcolor black -backgroundcolor green  $logMessage
		}
	}
}            

# Create new self-signed certificate
function CreateNewSelfSignedCertificate($cn){
	trap [Exception]{
			write-log "Error" "Could not create new self-signed certificate with CN=$cn"
	}

	$name = new-object -com "X509Enrollment.CX500DistinguishedName.1"
	$name.Encode("CN=$cn", 0)

	$key = new-object -com "X509Enrollment.CX509PrivateKey.1"
	$key.ProviderName = "Microsoft RSA SChannel Cryptographic Provider"
	$key.KeySpec = 1
	$key.Length = 1024
	$key.SecurityDescriptor = "D:PAI(A;;0xd01f01ff;;;SY)(A;;0xd01f01ff;;;BA)(A;;0x80120089;;;NS)"
	$key.MachineContext = 1
	$key.Create()

	$serverauthoid = new-object -com "X509Enrollment.CObjectId.1"
	$serverauthoid.InitializeFromValue("1.3.6.1.5.5.7.3.1")
	$ekuoids = new-object -com "X509Enrollment.CObjectIds.1"
	$ekuoids.add($serverauthoid)
	$ekuext = new-object -com "X509Enrollment.CX509ExtensionEnhancedKeyUsage.1"
	$ekuext.InitializeEncode($ekuoids)

	$cert = new-object -com "X509Enrollment.CX509CertificateRequestCertificate.1"
	$cert.InitializeFromPrivateKey(2, $key, "")
	$cert.Subject = $name
	$cert.Issuer = $cert.Subject
	$cert.NotBefore = get-date
	$cert.NotAfter = $cert.NotBefore.AddDays(90)
	$cert.X509Extensions.Add($ekuext)
	$cert.Encode()

	$enrollment = new-object -com "X509Enrollment.CX509Enrollment.1"
	$enrollment.InitializeFromRequest($cert)
	$certdata = $enrollment.CreateRequest(0)
	$enrollment.InstallResponse(2, $certdata, 0, "")
	
	write-log "Info" "Created new self-signed certificate with CN=$cn..."
}

# returns false if OS is not server SKU
 function NotServerOS{
    $sku = $((gwmi win32_operatingsystem).OperatingSystemSKU)
    $server_skus = @(7,8,9,10,12,13,14,15,17,18,19,20,21,22,23,24,25)
    
    return ($server_skus -notcontains $sku)
 }
 
 # gets path of applicationHost.config
 # WARNING: Will not work in shared config!!
 function GetApplicationHostConfigPath{
	return (${env:windir} + "\system32\inetsrv\config\applicationHost.config")
 }

 # gives a user access to an IIS site's scope
 function GrantAccessToSiteScope($username, $websiteName){
    trap [Exception]{
        write-log "Error" "Could not grant $username access to $websiteName"
    }
	
	[Microsoft.Web.Management.Server.ManagementAuthorization]::Grant($username, $websiteName, $FALSE) | out-null
	write-log "Info" "Granted access to $websiteName's scope to $username"
 }
 
 # gives a user permissions to a file on disk 
 function GrantPermissionsOnDisk($username, $path, $type, $options){
	trap [Exception]{
        write-log "Error" "Could not grant $type permissions on $path to $username with options $options"
    }

	$acl = (Get-Item $path).GetAccessControl("Access")
	$accessrule = New-Object system.security.AccessControl.FileSystemAccessRule($username, $type, $options, "None", "Allow")
	$acl.AddAccessRule($accessrule)
	set-acl -aclobject $acl $path
	write-log "Info" "Granted $type permissions on $path to $username"
}
 
 
 function CreateLocalUser($username, $password, $isAdmin){
    trap [Exception]{
        write-log "Error" "Could not create local user $username"
    }
    
	if (-not (CheckLocalUserExists($username) -eq $true)){
		$comp = [adsi] "WinNT://$env:computername"  
		$user = $comp.Create("User", $username)   
		$user.SetPassword($password)   
		$user.SetInfo()
		
		write-log "Info" "User did not exist, so created local user: $username with password: $password"
	}
	
	if($isAdmin){
		$group = [ADSI]"WinNT://$env:computername/Administrators,group"
		$group.add("WinNT://$env:computername/$username")
		write-log "Info" "Added $username to local Administrators group"
	}
 }
 
 function CheckLocalUserExists($username){
	$objComputer = [ADSI]("WinNT://$env:computername")
	$colUsers = ($objComputer.psbase.children | Where-Object {$_.psBase.schemaClassName -eq "User"} | Select-Object -expand Name)

	$blnFound = $colUsers -contains $username

	if ($blnFound){
		return $true
	}
	else{
		return $false
	}
 }
 
 function GetWebsitePhysicalPath($website){
    $path = ""
	$i = 0
	$found = $false
	for ($i=0; $i -lt $serverManager.Sites.Count; $i++){
		if($serverManager.Sites[$i].Name -eq $website){
			$found = $true
			break;            
		}        
	}
	if($found){
		$path = $serverManager.Sites[$i].Applications[0].virtualDirectories[0].physicalPath
	}
   
    return [System.Environment]::ExpandEnvironmentVariables($path)
 }
 
 function LoadAssemblies{
    trap [Exception]{
        write-log "Error" "Failed to load Microsoft.Web.*.dll. Are you sure IIS 7 is installed?"
        break
    }
    $global:mwaAssembly = [System.Reflection.Assembly]::LoadFrom( [System.Environment]::ExpandEnvironmentVariables("%WINDIR%") + 
                                                               "\system32\inetsrv\Microsoft.Web.Administration.dll" )
    $global:serverManager = (New-Object Microsoft.Web.Administration.ServerManager)
    $global:mwmAssembly = [System.Reflection.Assembly]::LoadFrom( [System.Environment]::ExpandEnvironmentVariables("%WINDIR%") + 
                                                               "\system32\inetsrv\Microsoft.Web.Management.dll" )
 }

function ReplaceTextInFile($filePath, $old, $new){
	(Get-Content $filePath) | 
	Foreach-Object {$_ -replace $old, $new} | 
	Set-Content $filePath
}

function GetPublicHostname(){
	$ipProperties = [System.Net.NetworkInformation.IPGlobalProperties]::GetIPGlobalProperties()
	if($ipProperties.DomainName -eq ""){
		return $ipProperties.HostName
	}
	else{
		return "{0}.{1}" -f $ipProperties.HostName, $ipProperties.DomainName
	}
}

function GetEC2PublicHostname(){
	trap [Exception]{
		write-log "Error" "Could not obtain public hostname from EC2"
		return "replace_with_public_hostname.com"
	}
	$saveName = "publicHostname.txt"
	$downloadUrl = "http://169.254.169.254/latest/meta-data/public-hostname"
	write-log "Info" "Obtaining public hostname for this instance from EC2 metadata sevice..."
	$webClient = new-object System.Net.WebClient
	$currentPath = get-location
	$savePath = join-path $currentPath $saveName
	$webClient.DownloadFile($downloadUrl, $savePath)
	$result = get-content $savePath
	remove-item $savePath
	write-log "Info" "Public hostname for this instance is $result"
	return $result
}

function GenerateStrongPassword(){
	$rand = New-Object System.Random
	1..5 | ForEach { $NewPassword = $NewPassword + [char]$rand.next(97,122) + [char]$rand.next(65,90) + [char]$rand.next(48,57) }
	return $NewPassword
}
export-modulemember LoadAssemblies,GetWebsitePhysicalPath,CreateLocalUser,GrantPermissionsOnDisk,CreateAndAuthorizeIISManagerUser,NotServerOS,GetApplicationHostConfigPath,GrantAccessToSiteScope,CreateWebsite,write-log,CreateNewSelfSignedCertificate,ReplaceTextInFile,GetEC2PublicHostname,GenerateStrongPassword,GetPublicHostname,WriteImportantMessage