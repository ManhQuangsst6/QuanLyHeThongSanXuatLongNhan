import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { useAuth } from "../provider/authProvider";
import { ProtectedRoute } from "./ProtectedRoute";
import { Link } from "react-router-dom";
import Login from "../login";
import Logout from "../logout";
import AttendanceComponent from "../Components/Atttendance/AtttendanceComponent"
import RegisterDayLonganComponent from "../Components/RegisterDayLongan/RegisterDayLonganComponent";
import RegisterRemainningLonganComponent from "../Components/RegisterRemainningLongan/RegisterRemainningLonganComponent";
import SalaryComponent from "../Components/Salary/SalaryComponent";
import Home from "../Components/Home/home";
const Routes = () => {
    const { token } = useAuth();

    // Define routes accessible only to authenticated users
    const routesForAuthenticatedOnly = [
        {
            path: "/",
            element: <ProtectedRoute />, // Wrap the component in ProtectedRoute
            children: [
                {
                    path: "/attendance",
                     element: <AttendanceComponent></AttendanceComponent>,
                },
                {
                    path: "/register-day-longan",
                    element: <RegisterDayLonganComponent></RegisterDayLonganComponent>,
                },
                {
                    path: "/register-remainning-longan",
                    element: <RegisterRemainningLonganComponent></RegisterRemainningLonganComponent>,
                },
                {
                    path: "/salary",
                    element: <SalaryComponent></SalaryComponent>,
                },
            ],
        },
    ];

    // Define routes accessible only to non-authenticated users
    const routesForNotAuthenticatedOnly = [
        {
            path: "/login",
            element: <Login></Login>,
        },
    ];

    const router = createBrowserRouter([

        ...(!token ? routesForNotAuthenticatedOnly : []),
        ...routesForAuthenticatedOnly,
    ]);

    return (<div>
        <RouterProvider router={router} >
        </RouterProvider>
    </div>)
        ;
};

export default Routes;