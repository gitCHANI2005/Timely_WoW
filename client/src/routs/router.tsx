import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import HomePage from '../pages/pageTSX/PageHome';
import Login from '../pages/pageTSX/Login'; // נתיב לדף ההרשמה
import CustomerSignUp from '../pages/pageTSX/CustomerSignUp';
import BusinessSignUp from '../pages/pageTSX/BusinessSignUp';
import DeliverySignUp from '../pages/pageTSX/DeliverySignUp';
// import AllORDERS from '../pages/pageTSX/AllOrders';
import DeliverMyOrders from '../pages/pageTSX/DeliveryOrders.jsx';
import RestaurantDishes from '../pages/pageTSX/RestaurantDishes';
import RestaurantPage from '../pages/pageTSX/RestaurantPage';

const AppRouter = () => {
  return (
    <Router>
      <Routes>
      <Route path="/" element={<HomePage />} />
        <Route path="/RestaurantDishes" element={<RestaurantDishes />} />
        <Route path="/login" element={<Login />} />
        <Route path="/signup/customer" element={<CustomerSignUp />} />
        <Route path="/signup/business" element={<BusinessSignUp />} />
        <Route path="/signup/delivery" element={<DeliverySignUp />} />
        <Route path="/RestaurantPage/:RestaurantId" element={<RestaurantPage/>} />
        <Route path="/DeliverMyOrders" element={<DeliverMyOrders/>}/>
      </Routes>
    </Router>
  );
};

export default AppRouter;
