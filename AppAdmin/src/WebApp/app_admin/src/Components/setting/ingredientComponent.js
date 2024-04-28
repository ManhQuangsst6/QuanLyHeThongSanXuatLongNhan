import React, { useState, useEffect } from 'react';
import { Space, Button, Table, Input, Form, Row, Col, DatePicker } from 'antd';
import { EditOutlined, DeleteOutlined, CaretRightOutlined, EyeOutlined, CheckOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { GetListAll, Post, Update, Remove } from '../../API/Ingredient/ingredient';
import { Modal } from 'antd';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
const { Column, ColumnGroup } = Table;
const { Search } = Input;




const { RangePicker } = DatePicker;
const { TextArea } = Input;


const IngredientComponent = () => {
    const columns = [
        {
            title: 'Tên',
            dataIndex: 'Name',
            width: '20%',
        },
        {
            title: 'Đơn vị',
            dataIndex: 'Measure',
            width: '20%',
        },

        {
            title: 'Description',
            dataIndex: 'Description',
            width: '40%',
        },
        {
            title: 'Tùy chọn',
            dataIndex: 'action',
            width: '20%',
            render: (_, record) => (
                <Space size="middle">
                    <a  ><EyeOutlined /></a>
                    <a onClick={() => showModal("EDIT", record)}><EditOutlined /></a>
                    <a onClick={() => DeleteAProject(record.key)}><DeleteOutlined /></a>
                </Space>
            ),
        },

    ];
    const navigate = useNavigate();
    const [data, SetData] = useState([]);
    const [isRender, SetIsRender] = useState(true);
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
            GetListAll().then(res => {
                console.log(res)
                let dataShow = res.data.value.map(item => {
                    return {
                        key: item.id,
                        Name: item.name,
                        Measure: item.measure,
                        Description: item.description
                    }
                })
                SetData(dataShow)
            })
        // GetProjects(filter.searchName, filter.filterDay, filter.filterMonth).then(res => {
        //     let dataShow = res.data.map(item => {
        //         return {
        //             key: item.ID,
        //             Name: item.Name,
        //             DateStart: item.DateStart,
        //             DateEnd: item.DateEnd,
        //             Description: item.Description
        //         }
        //     })
        //     SetData(dataShow)
        //     console.log(dataShow)
        // }).catch(e => {
        //     console.log(e)
        // })
        SetIsRender(false)
    }, [isRender])
    const DeleteAProject = (id) => {
        Remove(id).then(res => {
            if (res.data.isSuccess === true) {
                SetIsRender(true);
                notify("Xóa nguyên liệu")
            } else notifyError(res.data.message)
        }).catch(e => {
            notifyError(e);
        })
    }


    ////

    const [textTitle, SetTextTilte] = useState("")
    const [state, SetState] = useState("ADD")
    const [dataPush, SetDataPush] = useState({ ID: "", Name: '', Measure: '', Description: '' })
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
            SetTextTilte("Thêm mới nguyên liệu")
            ClearForm()
        }
        else if (state == "EDIT") SetTextTilte("Cập nhật nguyên liệu")
        if (dataEdit) {
            SetDataPush({
                ID: dataEdit.key,
                Name: dataEdit.Name,
                Measure: dataEdit.Measure,
                Description: dataEdit.Description
            })
        }
    };
    const ClearForm = () => {
        SetDataPush({ ID: "", Name: '', Measure: '', Description: '' })
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
                    notify("Thêm nguyên liệu")
                } else notifyError(res.data.message)

            }).catch(e => {
                notifyError(e)
            })
        } else {
            Update(dataPush).then(res => {
                if (res.data.isSuccess === true) {
                    SetIsRender(true);
                    notify("Thêm nguyên liệu")
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


    return (
        <div style={{ padding: 10 }}>

            <ToastContainer />
            <div style={{ marginTop: "16px", }}>

                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                    <h3>Danh sách nguyên liệu</h3>
                    <div style={{ display: 'flex', alignItems: 'center' }} >

                        <Button type="primary" ghost onClick={() => showModal("ADD")} style={{ marginRight: 16 }}>
                            Thêm
                        </Button>
                        <Modal title={textTitle} open={isModalOpen} onOk={handleOk} onCancel={handleCancel} >
                            <Form
                                layout="horizontal"
                                style={
                                    { maxWidth: 800 }
                                }
                            >

                                <Row>
                                    <Col span={24}>
                                        <Form.Item label="Tên nguyên liệu:">
                                            <Input name="Name" value={dataPush.Name} onChange={handleChange} />
                                        </Form.Item>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col span={24}>
                                        <Form.Item label="Đơn vị đo: ">
                                            <Input name="Measure" value={dataPush.Measure} onChange={handleChange} />
                                        </Form.Item>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col span={24}>
                                        <Form.Item label="Mô tả:">
                                            <TextArea name="Description" rows={4} value={dataPush.Description} onChange={handleChange} />
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
                        pageSize: 5
                    }}>


                </Table>
            </div>
        </div>

    )
}

export default IngredientComponent;