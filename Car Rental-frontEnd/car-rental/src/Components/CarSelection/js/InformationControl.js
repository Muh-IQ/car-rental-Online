import '../css/InformationControl.css'

export default function InformationControl({Price, DateFrom, DateTo, show }) {

    const dateFromObj = new Date(DateFrom);
    const dateToObj = new Date(DateTo);
    const timeDifference = dateToObj - dateFromObj;
    const dayDifference = timeDifference / (1000 * 60 * 60 * 24);
    
    
    let JSX = (show) ? AmountInfo(dayDifference,Price) : null;
    
    
    return (
        <div className="informations">

            <div className="date-info">
                <div className="start-end-date-continer">
                    <div className="from-to-line">To</div>
                    <div className="start-end-date">
                        <p className="start-date">{DateFrom}<span>10:00 am</span></p>
                        <p className="end-date">{DateTo} <span>10:00 am</span></p>
                    </div>
                </div>

                <div className="metadata">
                    <p className="total-days">Total Booking Days : <span>{dayDifference}</span> .</p>
                    <p className="delivery-location">The vehicle is delivered to the location of the company from which it was rented.</p>
                </div>
            </div>

            {JSX}

        </div>
    )
}

let AmountInfo = (dayDifference,Price) => {
    let TotalPrice = dayDifference * Price;
    return (
        <div className='amount-info'>
            <p>TOTAL RENTAL PRICE</p>
            <p className='totalPrice'>USD <span>{TotalPrice}</span></p>
            <p>FOR <span>{dayDifference}</span> DAYS</p>
            <hr />
            <div className='price-info-continer'>
                <div>
                    <p>Rental Cost</p>
                    <p>Extras</p>
                    <p>Total Rental Cost</p>
                </div>
                <div className='price-values'>
                    <p>USD <span>{TotalPrice}</span></p>
                    <p>USD <span>0</span></p>
                    <p>USD <span>{TotalPrice}</span></p>
                </div>
            </div>

        </div>
    )
}
