import { useEffect } from 'react';
import '../css/Home.css'
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {ErrorBody}  from '../../../Utility/SweetAlert.js'
export default function Home() {
    const navigate = useNavigate();
    let [date, setDate] = useState({Today:null, From: null, To: null });
    useEffect(() => {
        const today = new Date();
        const formattedDate = today.toISOString().split('T')[0];
        setDate({Today:formattedDate, From: formattedDate, To: formattedDate });
    }, [])

    let OnSearch = () => {
        if (date.Today > date.From || date.Today> date.To) {
            ErrorBody("Enter correct date","Error")
            return
        }
        if (date.To === date.From) {
            ErrorBody("At least one day must be selected","Error");
            return
        }
        
        navigate(`/Selection?Date_From=${date.From}&Date_To=${date.To}`)
    }

    return (

        <div className="backgound">
            <div className='title'>
                <h1>Mesaha</h1>
                <h4>Car Rental</h4>
            </div>
            <div className='container'>
                <div className='date'>
                    <div className='container-from'>
                        <label htmlFor='form'>Form</label>
                        <input id='form' type='date' value={date.From}
                            onChange={e => setDate({ ...date, From: e.target.value })}
                        />
                    </div>
                    <div className='container-to'>
                        <label htmlFor='to'>To</label>
                        <input id='to' type='date' value={date.To}
                            onChange={e => setDate({ ...date, To: e.target.value })}
                        />
                    </div>
                </div>
                <div onClick={ OnSearch} className='search'>Search</div>
            </div>
        </div>
    )
}




