import { useNavigate } from "react-router-dom";
import React, { useState } from 'react';
import ReactDOM from 'react-dom';
import { Form, Input, Button, Checkbox } from 'antd';
import { UserOutlined, LockOutlined } from '@ant-design/icons';
import { LoginUser } from './API/UserManager/userManager';
import { useAuth } from './provider/authProvider';
const PWD_REGEX = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()\-_=+{};:,<.>]).{6,}/

const USERNAME_REGEX = /^[A-Za-z][A-Za-z0-9_]{4,29}$/
const Login = () => {
    const { setToken } = useAuth();
    const navigate = useNavigate();
    const [user, SetUser] = useState({ UserName: '', Password: '' })
    const onFinish = values => {

    };
    const ValidateForm = () => {
        let checkRegex = USERNAME_REGEX.test(user.UserName) && PWD_REGEX.test(user.Password)
        if (!checkRegex) return false
        return true
    }
    const LoginUserForm = () => {
        if (ValidateForm()) {
        LoginUser(user).then((res) => {
            console.log(res)
            setToken(res.data.value.token)
            navigate("/attendance", { replace: true });
        })
        }
    }
    return (
        <div style={{ display: 'flex', justifyContent: 'center' }}>
            <Form style={{ marginTop: 80, border: '1px solid #999', padding: '40px 80px', borderRadius: 10, backgroundColor: '#eee' }}
                name="normal_login"
                className="login-form"
                initialValues={{
                    remember: true,
                }}
                onFinish={onFinish}
            >
                <Form.Item
                    name="username"
                    rules={[
                        {
                            required: true,
                            message: 'Please input your Username!',
                            pattern: USERNAME_REGEX,
                        },
                    ]}
                >
                    <Input prefix={<UserOutlined className="site-form-item-icon" />}
                        placeholder="Username"
                        value={user.UserName}
                        onChange={(e) => SetUser({ UserName: e.target.value, Password: user.Password })}
                    />
                </Form.Item>
                <Form.Item
                    name="password"
                    rules={[
                        {
                            required: true,
                            message: 'Please input your Password!',
                            pattern: PWD_REGEX,
                        },
                    ]}
                >
                    <Input
                        prefix={<LockOutlined className="site-form-item-icon" />}
                        type="password"
                        placeholder="Password"
                        value={user.Password}
                        onChange={(e) => SetUser({ UserName: user.UserName, Password: e.target.value })}
                    />
                </Form.Item>
                <Form.Item>
                    <Button type="primary" htmlType="submit" onClick={() => LoginUserForm()} className="login-form-button" style={{ width: '100%' }}>
                        Log in
                    </Button>
                </Form.Item>
                <Form.Item>
                    <a className="login-form-forgot" href="">
                        Forgot password
                    </a>

                </Form.Item>
            </Form>
        </div>

    );
};
export default Login;