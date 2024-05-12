import './style.scss'
import { Navigate, Outlet, NavLink, useNavigate } from "react-router-dom";
import { useAuth } from "../provider/authProvider";
import React, { useState } from "react";
import {
    MenuFoldOutlined,
    MenuUnfoldOutlined,
    ReadOutlined,
    UserOutlined,
    CarOutlined,
    SettingOutlined,
    GiftOutlined, HomeOutlined,
    AppstoreOutlined,BellOutlined 
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
    getItem(<NavLink to='/'>Home</NavLink>, '1', <HomeOutlined />),
    getItem('Nhân viên', 'sub1', <UserOutlined />, [
        getItem(<NavLink to='/employee/employee'>Nhân viên</NavLink>, '2'),
        getItem(<NavLink to='/employee/user'>Thợ thủ công</NavLink>, '3'),
    ]),
    getItem(<NavLink to='/attendance'>Chấm công</NavLink>, '4', <ReadOutlined />),
    getItem(<NavLink to='/register-day-longan'>Đặt nhãn </NavLink>, '8', <CarOutlined />),
    getItem(<NavLink to='/shipment'>Quản lý lô hàng</NavLink>, '5', <AppstoreOutlined />),
    getItem(<NavLink to='/PurchaseOrder'> Nhập nguyên liệu</NavLink>, '6', <AppstoreOutlined />),
    getItem(<NavLink to='/event'>Sự kiện</NavLink>, '7', <GiftOutlined />),
    getItem('Cài đặt', 'sub2', <SettingOutlined />, [
        getItem(<NavLink to='/setting/ingredient'>Nguyên liệu</NavLink>, '9'),
        getItem(<NavLink to='/setting/category'>Loại nhãn</NavLink>, '10'),
        getItem('Submenu', 'sub3', null, [getItem('Option 11', '11'), getItem('Option 12', '12')]),
    ]),
];
export const ProtectedRoute = () => {
    const { token } = useAuth();
    const [open, setOpen] = useState(false);
    const [textHeader, SetTextHeader] = useState('')
    const [collapsed, setCollapsed] = useState(false);
    const { setToken } = useAuth();
    const navigate = useNavigate();
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
                            ></Button><span className='text-header'>{textHeader}Trang chủ</span>
                        </div>
                       <div style={{display:'flex',alignItems:'center'}}>
                       <Badge count={5} >
                                 <BellOutlined style={{fontSize:30}}  />
                            </Badge>
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