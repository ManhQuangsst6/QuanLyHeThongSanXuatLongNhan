import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { useAuth } from "../provider/authProvider";
import { ProtectedRoute } from "./ProtectedRoute";
import { Link } from "react-router-dom";
import Login from "../login";
import Logout from "../logout";
import IngredientComponent from "../Components/setting/ingredientComponent";
import EmployeeComponent from "../Components/EmployeeManager/EmployeeComponent";
import UserComponent from "../Components/EmployeeManager/userComponent";
import ShipmentComponent from "../Components/shipment/shipmentComponnent";
import CategoryComponent from "../Components/setting/CategoryComponent";
import PurchaseOrderComponent from "../Components/PurchaseOrder/PurchaseOrdersComponent";
import EventComponent from "../Components/Event/EventComponent";
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
                    path: "/",
                    element: <Home></Home>,
                },
                {
                    path: "/employee/employee",
                    element: <EmployeeComponent></EmployeeComponent>,
                },
                {
                    path: "/employee/user",
                    element: <UserComponent></UserComponent>,
                },
                {
                    path: "/shipment",
                    element: <ShipmentComponent></ShipmentComponent>,
                },
                {
                    path: "/PurchaseOrder",
                    element: <PurchaseOrderComponent></PurchaseOrderComponent>,
                },
                {
                    path: "/Event",
                    element: <EventComponent></EventComponent>,
                },
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
                {
                    path: "/setting/ingredient",
                    element: <IngredientComponent></IngredientComponent>,
                },
                {
                    path: "/setting/category",
                    element: <CategoryComponent></CategoryComponent>,
                },
            ],
        },
    ];

    // Define routes accessible only to non-authenticated users
    const routesForNotAuthenticatedOnly = [
        // {
        //   path: "/",
        //   element: <div>Home Page</div>,
        // },
        {
            path: "/login",
            element: <Login></Login>,
        },
    ];

    // Combine and conditionally include routes based on authentication status
    const router = createBrowserRouter([

        ...(!token ? routesForNotAuthenticatedOnly : []),
        ...routesForAuthenticatedOnly,
    ]);

    // Provide the router configuration using RouterProvider
    return (<div>
        <RouterProvider router={router} >
        </RouterProvider>
    </div>)
        ;
};

export default Routes;