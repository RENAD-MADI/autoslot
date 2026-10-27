function validateLogin() {

    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;

    if (email === "" || password === "") {

        alert("Please fill in all login fields.");
        return false;
    }

    alert("Login successful.");
    return true;
}



function validateSignup() {

    let name = document.getElementById("name").value;
    let email = document.getElementById("email").value;
    let phone = document.getElementById("phone").value;
    let password = document.getElementById("password").value;
    let confirm = document.getElementById("confirm").value;

    if (name === "" || email === "" || phone === "" || password === "" || confirm === "") {

        alert("Please fill all fields.");
        return false;
    }

    if (password !== confirm) {

        alert("Passwords do not match.");
        return false;
    }

    alert("Account created successfully.");
    return true;
}



function validateBooking() {

    let name = document.getElementById("name").value;
    let phone = document.getElementById("phone").value;
    let serial = document.getElementById("serial").value;
    let date = document.getElementById("date").value;
    let time = document.getElementById("time").value;

    let message = document.getElementById("bookingMessage");

    if (name === "" || phone === "" || serial === "" || date === "" || time === "") {

        alert("Please fill all booking fields.");
        return false;
    }

    message.innerHTML = "Booking confirmed successfully.";
    message.style.color = "green";
    message.style.fontWeight = "bold";

    return true;
}



function changeServiceText() {

    let text = document.getElementById("dynamicText");

    text.innerHTML = "Today's recommended service is the Full Maintenance Package.";

    text.style.color = "#1e3a5f";
    text.style.fontWeight = "bold";
    text.style.backgroundColor = "#e8f5e9";
    text.style.padding = "10px";
    text.style.border = "1px solid green";
    text.style.borderRadius = "8px";
}



function showServices() {

    let services = [

        "Car Wash",
        "Oil Change",
        "Tire Check",
        "Battery Check",
        "Brake Inspection",
        "Engine Diagnosis",
        "AC Service",
        "Full Maintenance"

    ];

    let output = "";

    for (let i = 0; i < services.length; i++) {

        output += services[i] + "<br>";
    }

    document.getElementById("servicesOutput").innerHTML = output;
}



function changeServiceImage() {

    let image = document.getElementById("dynamicImage");

    image.src = "/images/maintenance.jpg";

    image.style.border = "4px solid green";
    image.style.transition = "0.3s";
}



function highlightButton(button) {

    button.style.backgroundColor = "#1e3a5f";
    button.style.color = "white";
}



function resetButton(button) {

    button.style.backgroundColor = "";
    button.style.color = "";
}