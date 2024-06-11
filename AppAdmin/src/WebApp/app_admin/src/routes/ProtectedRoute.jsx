import './style.scss'
import { Navigate, Outlet, NavLink, useNavigate ,useLocation} from "react-router-dom";
import { useAuth } from "../provider/authProvider";
import React, { useState,useEffect } from "react";
import {
    MenuFoldOutlined,
    MenuUnfoldOutlined,
    ReadOutlined,
    UserOutlined,
    CarOutlined,
    SettingOutlined, MoneyCollectOutlined,
    GiftOutlined, HomeOutlined,
    AppstoreOutlined, BellOutlined, SafetyCertificateOutlined
} from '@ant-design/icons';
import { Layout, Menu, Button, theme, Avatar, Popover, Badge } from 'antd';
import NotificationComponent from '../Components/Notification/NotificationComponent';

const { Header, Sider, Content } = Layout;

function getItem(label, key, icon, onClick,children) {
    return {
        key,
        icon,
        label,
        onClick,
        children
    };
}

export const ProtectedRoute = () => {
    const location = useLocation();
    const { token,role  } = useAuth();
    const [open, setOpen] = useState(false);
    const [textHeader, SetTextHeader] = useState('');
    const [collapsed, setCollapsed] = useState(false);
    const { setToken } = useAuth();
    const [filteredItems,SetfilteredItems]=useState([])
    const navigate = useNavigate();
    useEffect(() => {
        if(token){
            const currentItem = items.flatMap(item => item.children ? item.children : item)
            .find(item => item.label.props.to === location.pathname);
            SetfilteredItems( role === 'Manager' ? items : items.filter(item => item.key !== '/'));
        if (currentItem) {
            SetTextHeader(currentItem.onClick);
        }
        }
        
    }, [location]);
    const {
        token: { colorBgContainer, borderRadiusLG },
    } = theme.useToken();
    
    // Check if the user is authenticated
    if (!token) {
        return <Navigate to="/login" />;
    }

    const handleLogout = () => {
        setToken();
        navigate("/", { replace: true });
    };

    const handleOpenChange = (newOpen) => {
        setOpen(newOpen);
    };

    const updateHeader = (text) => {
        SetTextHeader(text);
    };
    
    const items = [
        getItem(<NavLink to='/'>Home</NavLink>, '/', <HomeOutlined />, () => updateHeader('Trang chủ')),
        getItem('Nhân viên', 'sub1', <UserOutlined />, null, [
            getItem(<NavLink to='/employee/employee'>Nhân viên</NavLink>, '/employee/employee', null, () => updateHeader('Nhân viên')),
            getItem(<NavLink to='/employee/user'>Thợ thủ công</NavLink>, '/employee/user', null, () => updateHeader('Thợ thủ công')),
        ]),
        getItem(<NavLink to='/attendance'>Chấm công</NavLink>, '/attendance', <ReadOutlined />, () => updateHeader('Chấm công')),
        getItem(<NavLink to='/register-day-longan'>Đặt nhãn </NavLink>, '/register-day-longan', <CarOutlined />, () => updateHeader('Đặt nhãn')),
        getItem(<NavLink to='/register-remainning-longan'>Thu hồi</NavLink>, '/register-remainning-longan', <SafetyCertificateOutlined />, () => updateHeader('Thu hồi')),
        getItem(<NavLink to='/shipment'>Quản lý lô hàng</NavLink>, '/shipment', <AppstoreOutlined />, () => updateHeader('Quản lý lô hàng')),
        getItem(<NavLink to='/PurchaseOrder'> Nhập nguyên liệu</NavLink>, '/PurchaseOrder', <AppstoreOutlined />, () => updateHeader('Nhập nguyên liệu')),
        getItem(<NavLink to='/event'>Sự kiện</NavLink>, '/event', <GiftOutlined />, () => updateHeader('Sự kiện')),
        getItem(<NavLink to='/salary'>Trả lương</NavLink>, '/salary', <MoneyCollectOutlined />, () => updateHeader('Trả lương')),
        getItem('Cài đặt', 'sub2', <SettingOutlined />, null, [
            getItem(<NavLink to='/setting/ingredient'>Nguyên liệu</NavLink>, '/setting/ingredient', null, () => updateHeader('Nguyên liệu')),
            getItem(<NavLink to='/setting/category'>Loại nhãn</NavLink>, '/setting/category', null, () => updateHeader('Loại nhãn')),
        ]),
    ];

    const content = (
        <div>
            <p onClick={() => handleLogout()}>Đăng xuất</p>
            <p>Thông tin cá nhân</p>
        </div>
    );

    // If authenticated, render the child routes
    return (
        <div>
            <Layout style={{ height: '100vh' }}>
                <Sider trigger={null} collapsible collapsed={collapsed}>
                    <div className="demo-logo-vertical" />
                    <Menu
                        theme="dark"
                        mode="inline"
                        defaultSelectedKeys={['1']}
                        selectedKeys={[location.pathname]}
                        items={filteredItems.map(item => ({
                            ...item,
                            onClick: item.children ? null : () => navigate(item.label.props.to)
                        }))}
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
                        <div style={{ display: 'flex', alignItems: 'center' }}>
                        <NotificationComponent />
                            <Popover
                                content={content}
                                trigger="click"
                                open={open}
                                onOpenChange={handleOpenChange}
                                style={{ background: '#eee' }}
                            >
                                <Avatar style={{ margin: 'auto 0', marginRight: 12, marginLeft: 32 }}
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
