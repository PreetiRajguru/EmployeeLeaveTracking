import React from 'react';
import { Navigate, Route } from 'react-router-dom';

function PrivateRoute({ isAuthenticated, ...props }: any) {
  return isAuthenticated ? <Route {...props} /> : <Navigate to="/login" />;
}

export default PrivateRoute;