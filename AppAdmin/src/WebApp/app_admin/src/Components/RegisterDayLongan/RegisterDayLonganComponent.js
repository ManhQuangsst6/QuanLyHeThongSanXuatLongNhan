import React, { useState, useEffect } from 'react';
import { Space, Button, Table, Input, Form, Row, Col, DatePicker, Flex } from 'antd';
import { EditOutlined, DeleteOutlined, ExclamationCircleFilled, EyeOutlined, PlusOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { GetListEmployeePage } from '../../API/Employee/EmployeeAPI';
import { GetListByPage,Remove,Post } from '../../API/RegisterDayLongan/RegisterDayLonganAPI';
import { Modal, Image, Select } from 'antd';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import dayjs from 'dayjs';
import ConvertDate from '../../Common/Convert/ConvertDate';
const { Column, ColumnGroup } = Table;
const { Search } = Input;
const { RangePicker } = DatePicker;
const { TextArea } = Input;
const { confirm } = Modal;


const RegisterDayLonganComponent = () => {
    const [nameSearch, SetNameSearch] = useState("")
    const columns = [
        {
            title: 'Ngày nhập',
            dataIndex: 'created',
            width: '15%',
        },
        {
            title: 'Mã nhân viên',
            dataIndex: 'employeeCode',
            width: '10%',
        },
        
        {
            title: 'Tên nhân viên',
            dataIndex: 'employeeName',
            width: '20%',
        },
        {
            title: 'Số lượng',
            dataIndex: 'amount',
            width: '20%',
        },
        {
            title: 'Trạng thái',
            dataIndex: 'status',
            width: '20%',
        },
        {
            title: 'Tùy chọn',
            dataIndex: 'action',
            width: '5%',
            render: (_, record) => (
                <Space size="middle" >
                    <a  ><EyeOutlined /></a>
                    <a onClick={() => showModal("EDIT", record)}><EditOutlined /></a>
                    <a onClick={() => DeletePurcharse(record.key)}><DeleteOutlined /></a>
                </Space>
            ),
        },

    ];
    const [form] = Form.useForm();
    const rules={
        title:[{required: true ,message:'Tiêu đề không bỏ trống'} ],
        startDate:[{required: true ,message:'Ngày không bỏ trống'} ],
        expense:[{required: true ,message:'Chi tiêu không bỏ trống'} ],
        employeeID:[{required: true ,message:'Tên nhân viên không bỏ trống'} ],
         }
    const [totalPassengers, setTotalPassengers] = useState(1);
    const [loading, setLoading] = useState(false);
    const [data, SetData] = useState([]);
    const [isRender, SetIsRender] = useState(true);
    const [selectedRowKeys, setSelectedRowKeys] = useState([]);
    const [listEmployee, SetListEmployee] = useState([])
    const [resetData, SetResetData] = useState(true)
    useEffect(() => {
        if (resetData) {
            GetListEmployeePage().then(res => {
                let data = res.data.value.map(item => {
                    return {
                        value: item.id,
                        label: item.employeeCode
                    }
                })
                SetListEmployee(data);
            })

        }
    }, [resetData])
    const onSelectChange = (newSelectedRowKeys) => {
        console.log('selectedRowKeys changed: ', newSelectedRowKeys);
        setSelectedRowKeys(newSelectedRowKeys);
    };
    const rowSelection = {
        selectedRowKeys,
        onChange: onSelectChange,
    };
    const hasSelected = selectedRowKeys.length > 0;
    useEffect(() => {
        if (isRender === true)
            fetchRecords(1, 10, nameSearch,dateSearch)
        SetIsRender(false)
    }, [isRender])
    const fetchRecords = (pageNum, pageSize, nameSearch,dateSearch) => {
        setLoading(true);
        GetListByPage({ pageNum, pageSize, nameSearch,dateSearch })
            .then((res) => {
                
                let dataShow = res.data.value.items.map(item => {
                    return {
                        key: item.id,
                        employeeID:item.employeeID,
                        employeeCode: item.employeeCode,
                        employeeName:item.employeeName,
                        amount:item.amount,
                        created: ConvertDate(item.created),
                        status: item.status,
                    }
                })
                console.log(dataShow)
                SetData(dataShow)
                setTotalPassengers(res.data.value.totalPages);
                setLoading(false);
            });
    };
    const DeletePurcharse = (id) => {
        confirm({
            title: 'Bạn có muốn xóa nhập hàng ?',
            icon: <ExclamationCircleFilled />,
            okText: 'Có',
            okType: 'danger',
            cancelText: 'Quay lại',
            onOk() {
                Remove(id).then(res => {
                    if (res.data.isSuccess === true) {
                        SetIsRender(true);
                        notify("Xóa đơn nhập")
                    } else notifyError(res.data.message)
                }).catch(e => {
                    notifyError(e);
                })
            },
            onCancel() {
            },
        });
    }

    const [textTitle, SetTextTilte] = useState("")
    const [state, SetState] = useState("ADD")
    const [dataPush, SetDataPush] = useState({
        id: "",
        title: "",
        startDate: null,
        expense: null,
        employeeID: "",
        description: ""
    })
    const [selectedItems, setSelectedItems] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);

    const notify = (message) => {
        toast.success(message + ' thành công!', {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "colored",
        });
    }
    const notifyError = (message) => {
        toast.error(message + '!', {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "colored",
        });
    }

    const showModal = () => {
        setIsModalOpen(true);
        SetTextTilte("Đăng kí số cân")
        ClearForm()
    };

    const ClearForm = () => {
        SetDataPush({
            id: "",
            title: "",
            startDate: "",
            expense: null,
            employeeID: "",
            description: ""
        })
    }
    
    const handleOk = () => {
        setIsModalOpen(false);
        if (state === "ADD") {
            console.log(dataPush)
            Post(dataPush).then(res => {
                if (res.data.isSuccess === true) {
                    SetIsRender(true);
                    notify("Thêm ")
                    form.resetFields()
                    ClearForm()
                } else notifyError(res.data.message)

            }).catch(e => {
                notifyError(e)
            })
        } 
    };
    const handleCancel = () => {
        setIsModalOpen(false); 
        ClearForm()
        form.resetFields()
    };

    const handleChange = (e) => {
        SetDataPush((dataPush) => ({
            ...dataPush,
            [e.target.name]: e.target.value,
        }));

    };

    const onSearch = (value, _e, info) => {
        SetNameSearch(value);
        SetIsRender(true)
    }

    const handleChangeEmployee = (value) => {
        SetDataPush({
            ...dataPush,
            employeeID: value
        })
    }

    const onChangeDatePicker = (date, dateString) => {
        console.log(date)
        SetDataPush({
            ...dataPush,
            startDate: date
        })
    };
    const [dateSearch,SetDateSearch]=useState(null)
    const onChangeDateSearch = (date, dateString) => {
        debugger
        console.log(date, dateString);
        SetDateSearch(date)
        fetchRecords(1, 10, nameSearch,date);
      };
    return (

        <div style={{ padding: 10 }}>
            <ToastContainer />
            <div style={{ marginTop: "16px", }}>

                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                <div style={{display:'flex'}}> 
                    <h3 style={{marginRight:20, lineHeight:"55px"}}>Danh sách sự kiện </h3>
                    <Search
                    placeholder="Nhập tên sự kiện"
                    allowClear
                    onSearch={onSearch}
                    style={{
                        width:400,
                       display:'flex',alignItems:'center'
                    }}
                />
                </div>
                <DatePicker placeholder='Chọn ngày' style={{height:34,margin:27}} onChange={onChangeDateSearch} />
                    <div style={{ display: 'flex', alignItems: 'center' }} >

                        <Button type="primary"  onClick={() => showModal("ADD")} style={{ marginRight: 16 }}
                        icon={<PlusOutlined />}>
                            Thêm
                        </Button>
                        <Modal title={textTitle} width={800} open={isModalOpen} onOk={form.submit} onCancel={handleCancel} >
                            <Form
                                layout="horizontal" form={form} onFinish={handleOk}
                            >
                                <Row>
                                  
                                    <Col span={11}>
                                        <Form.Item name="amount"  label="Số cân:" rules={rules.amount}>
                                            <Input name="amount" rows={4} value={dataPush.amount} onChange={handleChange}></Input>
                                        </Form.Item>
                                    </Col>
                                </Row>
                               
                            </Form>
                        </Modal>

                        {/* <ProjectModelComponent ></ProjectModelComponent> */}
                        {/* <Button type="primary" ghost style={{ marginRight: 16 }}>Thêm </Button> */}
                        <Button danger>Xóa </Button>
                    </div>
                </div>

                <div

                >

                    <span
                        style={{
                            marginLeft: 8,
                        }}
                    >
                        {hasSelected ? `Selected ${selectedRowKeys.length} items` : ''}
                    </span>
                </div>
                <Table rowSelection={rowSelection} columns={columns} dataSource={data}
                    pagination={{
                        pageSize: 10,
                        total: totalPassengers,
                        onChange: (page, pageSize) => {
                            fetchRecords(page, pageSize, nameSearch,dateSearch);
                        }
                    }}
                >
                </Table>
            </div>
        </div>
    )
}

export default RegisterDayLonganComponent;