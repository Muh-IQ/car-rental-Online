import '../css/CarSelection.css'
import InformationControl from '../js/InformationControl.js'
import Card from '../js/Card.js'
import { ErrorBody, Sure } from '../../../Utility/SweetAlert.js'

import { useRef, useState } from 'react';
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
export default function CarSelection() {
    const queryParams = new URLSearchParams(window.location.search);
    const dateFrom = queryParams.get('Date_From');
    const dateTo = queryParams.get('Date_To');


    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [vehicle, setVehicle] = useState([]);
    const [page, setPage] = useState(1);
    const [CountVehicle, setCountVehicle] = useState(0);


    const cardRef = useRef(null);

    useEffect(() => {
        const Data = async () => {
            setLoading(true);
            const [Data, status] = await FetchFromAPI(`https://localhost:7168/api/Vehicle/GetAllVehicle/pageIndex=${page}/pageSize=${4}`);
            if (status >= 300)
                SetError(status);
            else
                setVehicle(prevData => [...vehicle, ...Data]);

            setLoading(false);

        };
        Data();
    }, [page])



    useEffect(() => {
        const CountData = async () => {
            const [Data, status] = await FetchFromAPI(`https://localhost:7168/api/Vehicle/GetCountVehicle`);
            if (status >= 300)
                SetError(status);
            else
                setCountVehicle(Data);
        };
        CountData();
    }, [])




    useEffect(() => {
        const handleScroll = () => {
            if (cardRef.current) {
                let card = cardRef.current;
                if (card.scrollHeight - card.scrollTop <= card.clientHeight + 10 && !loading) {
                    console.log(vehicle.length);
                    setPage(prevPage => prevPage + 1);
                }
            }
        };
        if (cardRef.current) {
            cardRef.current.addEventListener('scroll', handleScroll);
        }
        return () => {
            if (cardRef.current) {
                cardRef.current.removeEventListener('scroll', handleScroll);
            }
        }
    }, [loading]);


    const GoToPaymentPage = (vehicleID) => {
        navigate(`/Selection/Payment?Date_From=${dateFrom}&Date_To=${dateTo}&Id=${vehicleID}`)
    }


    return (
        <div className="main-container">
            <div className="cards-container">
                <span className='count-car'>{CountVehicle} </span>
                <div ref={cardRef} id='cards-cont' className='cards'>
                    {GetVehicle(vehicle,GoToPaymentPage)}
                </div>
            </div>
            <div className="informations-container">
                <InformationControl DateFrom={dateFrom} DateTo={dateTo} show={false} />
            </div>
        </div>
    )
}



function GetVehicle(vehicle,GoToPaymentPage) {
    return (
        vehicle && vehicle.map(item => {
            return (< Card item={item} OnSelected={OnSelected} key={item.ID} GoToPaymentPage={GoToPaymentPage} />)
        })
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

async function OnSelected(FullNameCar, RentPerDay,GoToPaymentPage,vehicleID) {
    let result = await Sure(
        "Are you sure?",
        `${FullNameCar} has been selected and the rent per day is ${RentPerDay}$`
        , "Yes, select it!",
        "Selected!",
        "Your Item has been Selected."
    )
    if (result) GoToPaymentPage(vehicleID);
        
}