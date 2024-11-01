import '../css/Payment.css'
import InformationControl from '../../CarSelection/js/InformationControl.js'
import { useEffect, useState } from 'react';
import { ErrorBody, Sure, ProcessSuccessful } from '../../../Utility/SweetAlert.js'

let vehicleID;
export default function Payment() {

    const queryParams = new URLSearchParams(window.location.search);
    const dateFrom = queryParams.get('Date_From');
    const dateTo = queryParams.get('Date_To');
    const Id = queryParams.get('Id');


    const [vehicle, setVehicle] = useState();

    useEffect(() => {
        const Data = async () => {
            const [Data, status] = await FetchFromAPI(`https://localhost:7168/api/Vehicle/GetByID?id=${Id}`);
            if (status >= 300)
                SetError(status);
            else
                setVehicle(Data);


        };
        Data();
    }, [])

    let price = vehicle && vehicle.rentalPricePerDay
    vehicleID = vehicle && vehicle.vehicleID;
   
    const SaveProcess = (e) => {
        e.preventDefault();

        // التحقق من صيغة تاريخ انتهاء الصلاحية
        const expDate = document.getElementById("exp-date").value;
        if (!isValidDate(expDate)) {
            ErrorBody("You must enter the card expiration date in the format YYYY/MM/DD.")
            console.log("Invalid date format");
            return; // إذا كان التاريخ غير صحيح، اخرج من الدالة
        }

        Save(dateFrom, dateTo)
    }
    const isValidDate = (dateString) => {
        const regex = /^\d{4}-\d{2}-\d{2}$/; // التحقق من تنسيق yyyy-mm-dd
        return regex.test(dateString);
    }
    return (
        <form onSubmit={SaveProcess}>
            <div className="main-container">
                <div className="info-payment-container">
                    <span className='person-info-head'>Person Informations</span>
                    <div className='person-info'>
                        <div className='group'>
                            <label htmlFor="name">Name</label>
                            <input required type="text" id="name" name="name" />
                        </div>
                        <div className='group'>
                            <label htmlFor="age">Age</label>
                            <input required type="number" id="age" name="age" />
                        </div>
                        <div className='group'>
                            <label htmlFor="phone">Phone</label>
                            <input required type="text" id="phone" name="phone" />
                        </div>
                        <div className='group'>
                            <label htmlFor="email">Email</label>
                            <input type="email" id="email" name="email" />
                        </div>
                        <div className='group'>
                            <label htmlFor="address">Address</label>
                            <input required type="text" id="address" name="address" />
                        </div>
                        <div className='group'>
                            <label htmlFor="license">License</label>
                            <input required type="text" id="license" name="license" />
                        </div>
                    </div>
                    <span className='pay-info-head'>Payment Informations</span>
                    <div className='pay-info'>
                        <div className='group'>
                            <label htmlFor="card-number">Card Number</label>
                            <input required type="number" id="card-number" name="card-number" />
                        </div>
                        <div className='group'>
                            <label htmlFor="owner-name">Owner Name</label>
                            <input required type="text" id="owner-name" name="owner-name" />
                        </div>
                        <div className='group'>
                            <label htmlFor="exp-date">Exp Date</label>
                            <input required type="text" id="exp-date" placeholder='yyyy-mm-dd' name="exp-date" />
                        </div>
                        <div className='group'>
                            <label htmlFor="cvv">CVV</label>
                            <input required type="number" id="cvv" name="cvv" />
                        </div>
                    </div>

                    <div className='save'>
                        <button type="submit" id='save' className="submit-btn">OK</button>
                    </div>
                </div>
                <div className="informations-container">
                    <InformationControl Price={price} DateFrom={dateFrom} DateTo={dateTo} show={true} />

                </div>
            </div>
        </form>
    )
}

async function FetchFromAPI(Url) {
    const response = await fetch(Url);
    const Data = await response.json();
    return [Data, response.status];
}

function SetError(status) {
    switch (+status) {
        case 400:
            return ErrorBody("The data entered is incorrect")
        case 404:
            return ErrorBody("The search was not completed due to an error in the search elements")
        case 500:
            return ErrorBody("A server error occurred")
        default:
            return ErrorBody("An unknown error has occurred")
    }
}

async function Save(dateFrom, dateTo) {
    var res = await Sure(null, "This process cannot be undone!", "Yes, I am sure!")
    console.log(res);
    //If the cancel button is selected
    if (!res) return;

    const Url = `https://localhost:7168/api/Booking/Booking`;
    const [Data, status] = await PostToApi(Url,dateFrom, dateTo);
    if (status >= 300) {
        SetError(status);
        console.log(new Error("Error in Add booking"));
    }
    else if (!Data) {
        ErrorBody("An unexpected error occurred, please try again later");
        console.log(new Error("Error in Add booking"));
    }
    else
    {
        ProcessSuccessful();
        DisableSave();
    }
}

async function PostToApi(URL, dateFrom, dateTo) {

    var res = await fetch(URL, {
        method: "POST",
        headers: {
            "Content-Type": "Application/json"
        },
        body: GetValues(dateFrom, dateTo)
    })
    const Data = await res.json();
    const status = res.status;
    return [Data, status];
}

function GetValues(dateFrom, dateTo) {
    let name = document.getElementById("name").value;
    let email = document.getElementById("email").value;
    let phone = document.getElementById("phone").value;
    let driverLicenseNumber = document.getElementById("license").value;
    let address = document.getElementById("address").value; 
    let age = parseInt(document.getElementById("age").value, 10); // تحويل إلى رقم
    let paymentCardNumber = document.getElementById("card-number").value;
    let cvv = parseInt(document.getElementById("cvv").value, 10); // تحويل إلى رقم
    let expierDate = document.getElementById("exp-date").value;
    let cardOwnerName = document.getElementById("owner-name").value;
    
    // تأكد من تعريف vehicleID هنا قبل استخدامه أو احصل عليه من العنصر المناسب
    let vehicleId = parseInt(vehicleID, 10); // أو تحديده بأي طريقة أخرى

    const info = {
        name: name,
        email: email,
        phone: phone,
        driverLicenseNumber: driverLicenseNumber,
        address: address,
        age: age,
        paymentCardNumber: paymentCardNumber,
        cvv: cvv,
        expierDate: expierDate,
        cardOwnerName: cardOwnerName,
        vehicleID: vehicleId,
        rentalStartDate: dateFrom,
        rentalEndDate: dateTo
    };

    let json = JSON.stringify(info);
    
    return json;
}

const DisableSave =() =>
{
    let save = document.getElementById("save");
    if (save) save.disabled = true;
}