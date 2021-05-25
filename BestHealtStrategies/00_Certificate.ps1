# References:
#  - http://stam.blogs.com/8bits/2010/09/how-to-create-a-self-signed-ssl-certificate-with-powershell.html
#  - http://blogs.technet.com/b/vishalagarwal/archive/2009/08/22/generating-a-certificate-self-signed-using-powershell-and-certenroll-interfaces.aspx

# ==== import common functions ====
function Get-ScriptDirectory
{
 $Invocation = (Get-Variable MyInvocation -Scope 1).Value
 Split-Path $Invocation.MyCommand.Path
}
$commons = Join-Path (Get-ScriptDirectory) "HostingCommon.psm1";
import-module $commons;
# ==================================

if($global:loginitialized -eq $null){
	$global:loginitialized = $false
}

# Remove SSL binding of old wmsvc certificate on port 8172
netsh http delete sslcert ipport=0.0.0.0:8172
write-log "Info" "Deleted SSL binding of old self-signed certificate on port 8172"

# UNCOMMENT THIS AS NEEDED: Delete old WMSvc certificate from cert store, this isn't really required!
#$oldCertThumbprint = (Get-ChildItem cert:\LocalMachine\My | where-object { $_.Subject -like "CN=WMSvc*" } | Select-Object -First 1).Thumbprint
#remove-certificate cert:\localmachine\my\$oldCertThumbprint
#write-log "Info" "Deleted old wmsvc certificate from store"

# Create new self-signed certificate
CreateNewSelfSignedCertificate("WebMatrix-$env:computername")
$newCertThumbprint = (Get-ChildItem cert:\LocalMachine\My | where-object { $_.Subject -like "CN=WebMatrix*" } | Select-Object -First 1).Thumbprint

# bind the new certificate to the WMSvc port (default is 8172)
netsh http add sslcert ipport=0.0.0.0:8172 certhash=$newCertThumbprint appid=`{00000000-0000-0000-0000-000000000000`}
write-log "Info" "Bound new certificate to port 8172"

# set WebManagement registry keys with binary hash of new certificate so that new certificate is used by WMSvc
[Byte[]] $byteHash = $newCertThumbprint -split '([a-f0-9]{2})' | foreach-object { if ($_) {[System.Convert]::ToByte($_,16)}}
set-itemproperty -path hklm:\SOFTWARE\Microsoft\WebManagement\Server -name SslCertificateHash -value $byteHash -type binary
set-itemproperty -path hklm:\SOFTWARE\Microsoft\WebManagement\Server -name SelfSignedSslCertificateHash -value $byteHash -type binary
write-log "Info" "Updated wmsvc settings to use new certificate"

# stop wmsvc
Stop-Service wmsvc
write-log "Info" "Stopping Web Management Service..."

# start wmsvc
Start-Service wmsvc
write-log "Info" "Starting Web Management Service..."