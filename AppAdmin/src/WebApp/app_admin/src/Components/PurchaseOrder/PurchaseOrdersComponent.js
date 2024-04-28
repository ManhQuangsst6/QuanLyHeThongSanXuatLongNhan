import React, { useState, useEffect } from 'react';
import { Space, Button, Table, Input, Form, Row, Col, DatePicker, Flex } from 'antd';
import { EditOutlined, DeleteOutlined, ExclamationCircleFilled, EyeOutlined, CheckOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { GetListAll } from '../../API/Ingredient/ingredient';
import { GetListEmployeePage } from '../../API/Employee/EmployeeAPI';
import { GetListPurchaseOrdersByPage, Post, Update, GetByID, Remove } from '../../API/PurchaseOrder/purchaseOrderAPI';
import { Modal, Image, Select } from 'antd';
import { ToastContainer, toast } from 'react-toastify';
import { Typography } from 'antd';
import 'react-toastify/dist/ReactToastify.css';
import { render } from '@testing-library/react';
import dayjs from 'dayjs';
import ConvertDate from '../../Common/Convert/ConvertDate';
const { Column, ColumnGroup } = Table;
const { Search } = Input;




const { RangePicker } = DatePicker;
const { TextArea } = Input;
const { confirm } = Modal;


const PurchaseOrderComponent = () => {
    const [nameSearch, SetNameSearch] = useState("")
    const columns = [
        {
            title: 'Tên nguyên liệu',
            dataIndex: 'IngredientName',
            width: '20%',
        },
        {
            title: 'Mã nhân viên',
            dataIndex: 'EmployeeCode',
            width: '20%',
        },
        {
            title: 'Ngày nhập',
            dataIndex: 'OrderDate',
            width: '20%',
        },

        {
            title: 'Số cân',
            dataIndex: 'Amount',
            width: '10%',
        },
        {
            title: 'Giá',
            dataIndex: 'Price',
            width: '10%',
        },
        {
            title: 'Tổng tiền',
            dataIndex: 'Money',
            width: '15%',
            render: (_, record) => (
                <span>{record.Price * record.Amount}</span>
            ),
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
    const [totalPassengers, setTotalPassengers] = useState(1);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();
    const [data, SetData] = useState([]);
    const [isRender, SetIsRender] = useState(true);
    const [selectedRowKeys, setSelectedRowKeys] = useState([]);
    const [listEmployee, SetListEmployee] = useState([])
    const [listIngredientID, SetListIngredientID] = useState([])
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
                SetListIngredientID(data);
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
            fetchRecords(1, 10, nameSearch)
        SetIsRender(false)
    }, [isRender])
    const fetchRecords = (pageNum, pageSize, nameSearch) => {
        setLoading(true);
        GetListPurchaseOrdersByPage({ pageNum, pageSize, nameSearch })
            .then((res) => {
                console.log(res.data.value.totalPages)
                let dataShow = res.data.value.items.map(item => {
                    return {
                        key: item.id,
                        EmployeeCode: item.employeeCode,
                        OrderDate: ConvertDate(item.orderDate),
                        Amount: item.amount,
                        IngredientName: item.ingredientName,
                        Price: item.price
                    }
                })
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
        employeeID: "",
        orderDate: "",
        amount: null,
        note: "",
        price: null,
        ingredientID: ""
    })
    const [selectedItems, setSelectedItems] = useState([]);
    //const filteredOptions = OPTIONS.filter((o) => !selectedItems.includes(o));
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
        console.log(dataEdit)
        if (state == "ADD") {
            SetTextTilte("Thêm mới lô hàng")
            ClearForm()
        }
        else if (state == "EDIT") SetTextTilte("Cập nhật lô hàng")
        if (dataEdit) {
            GetByID(dataEdit.key).then(res => {

                SetDataPush({
                    ...dataPush,
                    ...res.data.value,
                    orderDate: dayjs(res.data.value.orderDate)
                })
                // SetDataPush({
                //     ...dataPush,
                //     orderDate: dayjs(res.data.value.orderDate)
                // })
                console.log(dataPush)
            })

        }
    };

    const ClearForm = () => {
        SetDataPush({
            id: "",
            employeeID: "",
            orderDate: "",
            amount: null,
            note: "",
            price: null,
            ingredientID: ""
        })
    }
    const UpdateCompleteProject = (id) => {
        // UpdateComplete(id).then(res => {
        //     SetIsRender(true)
        //     notify("Dự án đã kết thúc")
        // }).catch(e => {
        //     console.log(e)
        // })
    }
    const handleOk = () => {
        setIsModalOpen(false);
        if (state === "ADD") {
            console.log(dataPush)
            Post(dataPush).then(res => {
                if (res.data.isSuccess === true) {
                    SetIsRender(true);
                    notify("Thêm ")
                } else notifyError(res.data.message)

            }).catch(e => {
                notifyError(e)
            })
        } else {
            Update(dataPush).then(res => {
                if (res.data.isSuccess === true) {
                    SetIsRender(true);
                    notify("Cập nhật ")
                } else notifyError(res.data.message)
            }).catch(e => {
                notifyError(e)
            })
        }
        ClearForm()
    };
    const handleCancel = () => {
        setIsModalOpen(false); ClearForm()
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
    const handleChangeSelectModal = (value) => {
        console.log(value)
        SetDataPush({
            ...dataPush,
            role: value
        })

    };
    const handleChangeEmployee = (value) => {
        SetDataPush({
            ...dataPush,
            employeeID: value
        })
    }
    const handleChangeingredient = (value) => {
        SetDataPush({
            ...dataPush,
            ingredientID: value
        })
    }
    const onChangeDatePicker = (date, dateString) => {
        SetDataPush({
            ...dataPush,
            orderDate: date
        })
    };
    return (

        <div style={{ padding: 10 }}>
            <div style={{ display: 'flex', alignItems: 'center', border: '1px solid #eee', padding: 12 }}>
                {/* <b style={{ marginRight: 6 }}></b> */}
                <Search
                    placeholder="Mã lô hàng"
                    allowClear
                    onSearch={onSearch}
                    style={{
                        width: 400,
                    }}
                />
            </div>

            <ToastContainer />
            <div style={{ marginTop: "16px", }}>

                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                    <h3>Danh sách lô hàng </h3>
                    <div style={{ display: 'flex', alignItems: 'center' }} >

                        <Button type="primary" ghost onClick={() => showModal("ADD")} style={{ marginRight: 16 }}>
                            Thêm
                        </Button>
                        <Modal title={textTitle} width={800} open={isModalOpen} onOk={handleOk} onCancel={handleCancel} >
                            <Form
                                layout="horizontal"
                                style={
                                    { maxWidth: 800 }
                                }
                            >


                                <Row>
                                    <Col span={12}>
                                        <Form.Item label="Nhân viên:">
                                            <Select
                                                value={dataPush.employeeID}
                                                onChange={handleChangeEmployee}
                                                options={listEmployee}
                                            />
                                        </Form.Item>
                                    </Col>
                                    <Col span={1}></Col>
                                    <Col span={11}>
                                        <Form.Item label="Nguyên liệu nhập:">
                                            <Select
                                                value={dataPush.ingredientID}
                                                onChange={handleChangeingredient}
                                                options={listIngredientID}
                                            />
                                        </Form.Item>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col span={12}>
                                        <Form.Item label="Ngày nhập:">
                                            <DatePicker value={dataPush.orderDate} style={{ width: '100%' }} onChange={onChangeDatePicker}></DatePicker>
                                        </Form.Item>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col span={12}>
                                        <Form.Item label="Số lượng:">
                                            <Input name="amount" rows={4} value={dataPush.amount} onChange={handleChange}></Input>
                                        </Form.Item>
                                    </Col>
                                    <Col span={1}>
                                    </Col>
                                    <Col span={11}>
                                        <Form.Item label="Đơn giá:">
                                            <Input name="price" rows={4} value={dataPush.price} onChange={handleChange}></Input>
                                        </Form.Item>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col span={24}>
                                        <Form.Item label="ghi chú:">
                                            <TextArea name="note" rows={4} value={dataPush.note} onChange={handleChange} />
                                        </Form.Item>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col span={18}></Col>
                                    <Col span={6}>
                                        <Form.Item label="Tổng tiền:" style={{ fontWeight: 700 }}>
                                            {(dataPush.amount > 0 && dataPush.price > 0) &&
                                                <Typography.Text style={{ fontWeight: 700 }}>{dataPush.amount * dataPush.price}</Typography.Text>}
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
                            fetchRecords(page, pageSize, nameSearch);
                        }
                    }}
                >


                </Table>
            </div>
        </div>

    )
}

export default PurchaseOrderComponent;