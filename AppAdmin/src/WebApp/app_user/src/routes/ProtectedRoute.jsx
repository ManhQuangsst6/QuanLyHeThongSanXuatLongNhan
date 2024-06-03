import './style.scss'
import { Navigate, Outlet, NavLink, useNavigate,useLocation  } from "react-router-dom";
import { useAuth } from "../provider/authProvider";
import React, { useState ,useEffect} from "react";
import NotificationComponent from '../Components/Notification/NotificationComponent';
import {
    MenuFoldOutlined,
    MenuUnfoldOutlined,
    ReadOutlined,
    UserOutlined,
    CarOutlined,
    SettingOutlined,MoneyCollectOutlined,
    GiftOutlined, HomeOutlined,
    AppstoreOutlined,BellOutlined ,SafetyCertificateOutlined
} from '@ant-design/icons';
import { Layout, Menu, Button, theme, Avatar, Popover,Badge } from 'antd';
const { Header, Sider, Content } = Layout;
const { SubMenu } = Menu;
function getItem(label, key, icon, children, type) {
    return {
        key,
        icon,
        children,
        label,
        type,
    };
}
const items = [
    getItem(<NavLink to='/attendance'>Home</NavLink>, '1', <HomeOutlined />),
    getItem(<NavLink to='/register-day-longan'>Đặt nhãn </NavLink>, '8', <CarOutlined />),
    getItem(<NavLink to='/register-remainning-longan'>Thu hồi</NavLink>, '15', <SafetyCertificateOutlined />),
    getItem(<NavLink to='/salary'>Trả lương</NavLink>, '16', <MoneyCollectOutlined />),
];
export const ProtectedRoute = () => {
    const [selectedKeys, setSelectedKeys] = useState(['1']);
    const { token } = useAuth();
    const [open, setOpen] = useState(false);
    const [textHeader, SetTextHeader] = useState('')
    const [collapsed, setCollapsed] = useState(false);
    const { setToken } = useAuth();
    const location = useLocation(); 
    const navigate = useNavigate();
    useEffect(() => {
        switch (location.pathname) {
            case '/attendance':
                setSelectedKeys(['1']);
                SetTextHeader('Trang chủ');
                break;
            case '/register-day-longan':
                setSelectedKeys(['8']);
                SetTextHeader('Đặt nhãn');
                break;
            case '/register-remainning-longan':
                setSelectedKeys(['15']);
                SetTextHeader('Thu hồi');
                break;
            case '/salary':
                setSelectedKeys(['16']);
                SetTextHeader('Trả lương');
                break;
            default:
                setSelectedKeys(['1']);
                SetTextHeader('Trang chủ');
                break;
        }
    }, [location.pathname]);
    const {
        token: { colorBgContainer, borderRadiusLG },
    } = theme.useToken();
    // Check if the user is authenticated
    if (!token) {
        // If not authenticated, redirect to the login page
        return <Navigate to="/login" />;
    }
    const handleLogout = () => {
        setToken();
        navigate("/", { replace: true });
    };
    const handleOpenChange = (newOpen) => {
        setOpen(newOpen);
    };
    
    const content = (
        <div>
            <p onClick={() => handleLogout()}>Đăng xuất</p>
            <p>Thông tin cá nhân</p>
        </div>
    );
    // If authenticated, render the child routes
    return (
        <div >
            <Layout style={{ height: '100vh' }}>
                <Sider trigger={null} collapsible collapsed={collapsed}>
                    <div className="demo-logo-vertical" />
                    <Menu
                        theme="dark"
                        mode="inline"
                        selectedKeys={selectedKeys}
                        defaultSelectedKeys={['1']}
                        items={items}
                    >
                    </Menu>
                </Sider>
                <Layout>
                    <Header
                        style={{
                            padding: 0,
                            background: colorBgContainer,
                            display: 'flex',
                            justifyContent: 'space-between'
                        }}
                    >
                        <div>
                            <Button
                                type="text"
                                icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
                                onClick={() => setCollapsed(!collapsed)}
                                style={{
                                    fontSize: '16px',
                                    width: 64,
                                    height: 64,
                                }}
                            ></Button><span className='text-header'>{textHeader}</span>
                        </div>
                       <div style={{display:'flex',alignItems:'center'}}>
                       <NotificationComponent />

                            <Popover
                            content={content}
                            trigger="click"
                            open={open}
                            onOpenChange={handleOpenChange}
                            style={{ background: '#eee' }}
                        >
                            <Avatar style={{ margin: 'auto 0', marginRight: 12 ,marginLeft:32}}
                                src="https://d1hjkbq40fs2x4.cloudfront.net/2016-01-31/files/1045-2.jpg" size="large" icon={<UserOutlined />} />
                        </Popover>
                       </div>
                        
                    </Header>
                    <Content
                        style={{
                            margin: '24px 16px',
                            padding: 24,
                            minHeight: 280,
                            background: colorBgContainer,
                            borderRadius: borderRadiusLG,
                        }}
                    >
                        <Outlet />
                    </Content>
                </Layout>
            </Layout>


        </div>

    );
};