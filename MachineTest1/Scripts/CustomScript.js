$(function () {
    $('.datetimepicker').datetimepicker({
        format: 'LL'
        ,maxDate: new Date()
    });
});

function findAge() {
    var day = document.getElementById("DOB").value;
    var dob = new Date(day);
    var today = new Date();
    var age = today.getTime() - dob.getTime();
    age = Math.floor(age / (1000 * 60 * 60 * 24 * 365.25));
    if (isNaN(age)) age = 0;
    document.getElementById("Age").value = age;
}