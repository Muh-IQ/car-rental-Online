import Home from './Components/Home/js/Home.js';
import {
  BrowserRouter as Router,
  Route,
  Routes
} from "react-router-dom";
import Selection from './Components/CarSelection/js/CarSelection.js';
import Payment from './Components/Payment/js/Payment.js';
import './App.css';

function App() {
  return (
    <Router>
      <Routes>
        <Route path='/' element={<Home />} />
        <Route path='/Selection' element={<Selection />} />
        <Route path='/Selection/Payment' element={<Payment />} />
      </Routes>
    </Router>
  );
}

export default App;