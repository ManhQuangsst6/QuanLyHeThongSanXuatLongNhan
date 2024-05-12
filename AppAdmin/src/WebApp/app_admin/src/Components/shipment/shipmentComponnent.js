import React, { useState, useEffect } from 'react';
import { Space, Button, Table, Input, Form, Row, Col, DatePicker, Flex } from 'antd';
import { EditOutlined, DeleteOutlined, ExclamationCircleFilled, EyeOutlined, PlusOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { } from '../../API/Ingredient/ingredient';
import { GetListEmployeePage } from '../../API/Employee/EmployeeAPI';
import { GetListShipmentByPage, GetByID, Post, Update, Remove } from '../../API/Shipment/shipmentAPI';
import { GetListAll } from '../../API/Category/categoryAPI'
import { Modal, Image, Select } from 'antd';
import { ToastContainer, toast } from 'react-toastify';
import dayjs from 'dayjs';
import ConvertDateCode from '../../Common/Convert/ConvertDateCode';
import 'react-toastify/dist/ReactToastify.css';
import { render } from '@testing-library/react';

const { Column, ColumnGroup } = Table;
const { Search } = Input;
const { confirm } = Modal;




const { RangePicker } = DatePicker;
const { TextArea } = Input;


const ShipmentComponent = () => {
    const dateFormat = 'YYYY/MM/DD HH:mm:ss';
    const [nameSearch, SetNameSearch] = useState("")
    const columns = [
        {
            title: 'Mã lô hàng',
            dataIndex: 'ShipmentCode',
            width: '20%',
        },
        {
            title: 'Mã nhân viên',
            dataIndex: 'EmployeeCode',
            width: '35%',
        },

        {
            title: 'Số cân',
            dataIndex: 'Amount',
            width: '20%',
        },
        {
            title: 'Trạng thái',
            dataIndex: 'Status',
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
                    <a onClick={() => OpenModalDelete(record.key, record.ShipmentCode)}><DeleteOutlined /></a>
                </Space>
            ),
        },

    ];
    const [form] = Form.useForm();
    const rules={
        employeeID:[{required: true ,message:'Tên người dùng không bỏ trống'} ],
        shipmentCode:[{required: true ,message:'Mã lô hàng không bỏ trống '} ],
        amount:[{required: true ,message:'Số cân không bỏ trống'} ],
        date:[{required: true ,message:'Thời gian không bỏ trống'} ],
        categoryID:[{required: true ,message:'Loại nhãn không bỏ trống'} ],
         }
    const [totalPassengers, setTotalPassengers] = useState(1);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();
    const [data, SetData] = useState([]);
    const [isRender, SetIsRender] = useState(true);
    const [listEmployee, SetListEmployee] = useState([])
    const [listCategory, SetListCategory] = useState([])
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
            GetListAll().then(res => {
                let data = res.data.value.map(item => {
                    return {
                        value: item.id,
                        label: item.name
                    }
                })
                SetListCategory(data);
            })
        }
    }, [resetData])
    const [selectedRowKeys, setSelectedRowKeys] = useState([]);
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
            fetchRecords(1, 10, nameSearch)
        SetIsRender(false)
    }, [isRender])
    const fetchRecords = (pageNum, pageSize, nameSearch) => {
        setLoading(true);
        GetListShipmentByPage({ pageNum, pageSize, nameSearch })
            .then((res) => {
                console.log(res.data.value.items)
                let dataShow = res.data.value.items.map(item => {
                    return {
                        key: item.id,
                        EmployeeCode: item.employeeCode,
                        ShipmentCode: item.shipmentCode,
                        Status: item.statusString,
                        Amount: item.amount
                    }
                })
                SetData(dataShow)
                setTotalPassengers(res.data.value.totalPages);
                setLoading(false);
            });
    };

    const OpenModalDelete = (id, code) => {
        confirm({
            title: 'Bạn có muốn xóa lô hàng ' + code + " ?",
            icon: <ExclamationCircleFilled />,
            okText: 'Có',
            okType: 'danger',
            cancelText: 'Quay lại',
            onOk() {
                Remove(id).then(res => {
                    if (res.data.isSuccess === true) {
                        SetIsRender(true);
                        notify("Xóa lô hàng")
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
    const [dataPush, SetDataPush] = useState(
        {
            ID: "",
            shipmentCode: '',
            dateFrom: '',
            dateTo: '',
            employeeID: '',
            categoryID: '',
            amount: 0
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

    const showModal = (state, dataEdit) => {
        setIsModalOpen(true);
        SetState(state)
        if (state == "ADD") {
            SetTextTilte("Thêm mới lô hàng")
            ClearForm()
            SetDataPush({
                ...dataPush,
                shipmentCode: ConvertDateCode(new Date())
            })
        }
        else if (state == "EDIT") SetTextTilte("Cập nhật lô hàng")
        if (dataEdit) {
            console.log(dataEdit)
            GetByID(dataEdit.key).then(res => {
                SetDataPush(res.data.value)
                SetDataRangDate([dayjs(res.data.value.dateTo, dateFormat), dayjs(res.data.value.dateFrom, dateFormat)])
                console.log(dataPush)
                form.setFieldValue("shipmentCode",res.data.value.shipmentCode)
                form.setFieldValue("date",[dayjs(res.data.value.dateTo, dateFormat), dayjs(res.data.value.dateFrom, dateFormat)])
               // form.setFieldValue("dateTo",dataEdit.dateTo)
                form.setFieldValue("employeeID",res.data.value.employeeID)
                form.setFieldValue("categoryID",res.data.value.categoryID)
                form.setFieldValue("amount",res.data.value.amount)
            })
        } 
    };
    const ClearForm = () => {
        SetDataPush({
            ID: "",
            shipmentCode: '',
            dateFrom: '',
            dateTo: '',
            employeeID: '',
            categoryID: '',
            amount: 0
        })
    }
    const handleOk = () => {
        setIsModalOpen(false);
        if (state === "ADD") {
            Post(dataPush).then(res => {
                if (res.data.isSuccess === true) {
                    SetIsRender(true);
                    notify("Thêm ")
                    ClearForm()
                    form.resetFields()

                } else notifyError(res.data.message)

            }).catch(e => {
                notifyError(e)
            })
        } else {
            Update(dataPush).then(res => {
                if (res.data.isSuccess === true) {
                    SetIsRender(true);
                    notify("Cập nhật")
                    ClearForm()
                    form.resetFields()
                } else notifyError(res.data.message)
            }).catch(e => {
                notifyError(e)
            })

        }
        ClearForm()
    };
    const handleCancel = () => {
        setIsModalOpen(false); ClearForm()
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
    const ChangeRangePicker = (listValue) => {
        dataPush.dateFrom = listValue[0]
        dataPush.dateTo = listValue[1]
        SetDataRangDate(listValue)
    }
    const handleChangeEmployee = (value) => {
        SetDataPush({
            ...dataPush,
            employeeID: value
        })
    }
    const handleChangeCategory = (value) => {
        SetDataPush({
            ...dataPush,
            categoryID: value
        })
    }
    const [DataRangDate, SetDataRangDate] = useState([]);

    return (

        <div style={{ padding: 10 }}>
            <ToastContainer />
            <div style={{ marginTop: "16px", }}>
                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                   <div style={{display:'flex'}}> 
                    <h3 style={{marginRight:20}}>Danh sách lô hàng </h3>
                    <Search
                    placeholder="Nhập mã lô hàng"
                    allowClear
                    onSearch={onSearch}
                    style={{
                        width:400,
                       display:'flex',alignItems:'center'
                    }}
                />
                </div>
                    <div style={{ display: 'flex', alignItems: 'center' }} >

                        <Button type="primary" onClick={() => showModal("ADD")} style={{ marginRight: 16 }}
                         icon={<PlusOutlined />}>
                            Thêm
                        </Button>
                        <Modal width={800} title={textTitle} open={isModalOpen} onOk={form.submit} onCancel={handleCancel} >
                            <Form
                                layout="horizontal"
                                form={form} onFinish={handleOk}
                            >
                                <Row>
                                    <Col span={12}>
                                        <Form.Item name="shipmentCode" label="Mã lô hàng:" rules={rules.shipmentCode}>
                                            <Input name="shipmentCode" value={dataPush.shipmentCode} onChange={handleChange} />
                                        </Form.Item>
                                    </Col>
                                    <Col span={1}></Col>
                                    <Col span={11}>
                                        <Form.Item label="Người lập:" name="employeeID" rules={rules.employeeID}> 
                                            <Select
                                                value={dataPush.employeeID}
                                                onChange={handleChangeEmployee}
                                                options={listEmployee}
                                            />
                                        </Form.Item>

                                    </Col>
                                </Row>
                                <Row>
                                    <Col span={24}>
                                        <Form.Item label="Thời gian:" name="date" rules={rules.date}> 
                                            {/* value={dataPush.dateFrom,dateTo} */}
                                            <RangePicker value={DataRangDate} onChange={ChangeRangePicker} />
                                        </Form.Item>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col span={12}>
                                        <Form.Item label="Loại nong:" name="categoryID" rules={rules.categoryID}>
                                            <Select
                                                value={dataPush.categoryID}
                                                onChange={handleChangeCategory}
                                                options={listCategory}
                                            />
                                        </Form.Item>
                                    </Col>
                                    <Col span={1}></Col>
                                    <Col span={11}>
                                        <Form.Item label="Khối lượng:" name="amount" rules={rules.amount}>
                                            <Input name="amount" value={dataPush.amount} onChange={handleChange} />
                                        </Form.Item>
                                    </Col>
                                </Row>
                            </Form>
                        </Modal>
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
                            fetchRecords(page, pageSize, nameSearch);
                        }
                    }}
                >


                </Table>
            </div>
        </div>

    )
}

export default ShipmentComponent;