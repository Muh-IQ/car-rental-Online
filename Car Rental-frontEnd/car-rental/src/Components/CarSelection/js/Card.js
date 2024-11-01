import '../css/Card.css';

export default function Card({ item, OnSelected ,GoToPaymentPage}) {

    
    return (
        <div className='card'>
            <div className='car-info'>
                <div className='header'>
                    <div className='logo' style={{ backgroundImage: `url(https://localhost:7168/api/Company/GetVehicleLogo/FileName=${item.company.logoName})` }}></div>
                    <span className='car-title'>{item.company.name + " " + item.model + " " + item.year}</span>
                </div>
                <div className='main-info'>
                    <div>
                        <p ><span>{item.seats}</span> Seats</p>
                        <p >{item.airCondition ? "" : "Not "}Air Condition</p>
                        <p ><span>{item.airCondition ? "Automatic" : "Manual"}</span> Transmission</p>
                    </div>
                    <div>
                        <p ><span>{item.doors}</span> Doors</p>
                        <p >{item.category.categoryName}</p>
                        <p ><span>{item.fule.fuleType}</span> Fule</p>
                    </div>
                </div>
                <div className='footer'>
                    <div onClick={() => OnSelected(`${item.company.name} ${item.model} ${item.year}`
                        , item.rentalPricePerDay,GoToPaymentPage,item.vehicleID)} className='select-btn'>Select</div>
                    <div className='price-per-day-in-footer'>Price per day <span>{item.rentalPricePerDay}</span>$</div>
                </div>

            </div>
            <div className='car-img' style={{ backgroundImage: `url(https://localhost:7168/api/Vehicle/GetVehicleImage/FileName=${item.path})` }} ></div>
        </div>
    );
}


