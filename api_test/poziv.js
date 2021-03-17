$.ajax({
  type: "GET",
  url: "https://api.edamam.com/search?q=&app_id=a0d42e61&app_key=8512cc537eb4edfb5ac3772c24bfea5c&mealType=breakfast&diet=high-protein&Health=alcohol-free, gluten-free, low-sugar&ingr=5",
  /*data: { 
    q: "egg", 
    app_id : "a0d42e61",
    app_key : "8512cc537eb4edfb5ac3772c24bfea5c",
    mealType : "breakfast",
    diet : "high-protein",
    Health : "alcohol-free, gluten-free, low-sugar",
    ingr : "5",
  },*/
  success: function (result, textStatus, jqXHR) {       
    if(result.length < 1){
        alert("Error");
        return
    }

    let obj = JSON.parse(jqXHR.responseText)
    console.log(obj);
    document.getElementById('json').innerHTML = JSON.stringify(obj, null, "\t");

    // $('#json').text(jqXHR.responseText.replace(/\r\n/g, EOL));
  },
  error: function(xhr, textStatus, errorThrown){
    alert("Error in getting document " + textStatus);
  } 
});