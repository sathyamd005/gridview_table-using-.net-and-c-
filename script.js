function test() {
    var fname = document.getElementById("firstname");
    var lname = document.getElementById("lastname");
    var address = document.getElementById("address");
    var male = document.getElementById("male");
    var female = document.getElementById("female");
    var pin = document.getElementById("pin")

   
    if (fname.value == "")
    {
        alert("Please Enter your FirstName");
        return false;
    }
    if( lname.value==""){
        alert("Please Enter your LastName");

        return false;
    }
    if (address.value == "") {
        alert("Please Enter your Address")
        return false;
    }
   

    if (document.getElementById('male').checked==false && document.getElementById('female').checked==false)
    {
        alert("Please Enter Your Gender")
        return false;
    }
    if(document.getElementById('country').value==""){
        alert("Please Select Your Country ")
        return false;
    }
    if (pin.value == "") {
        alert("Please Enter your pin")
        return false;
    }

    return true;
    
}
function view_Click() {
    Visible = "true"
}
function foo() {
    return confirm("Do You Want to Delete Entire data? ")
}