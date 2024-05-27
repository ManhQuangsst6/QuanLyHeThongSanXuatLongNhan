import React, { useState, useEffect } from "react";
import {
  Space,
  Button,
  Table,
  Input,
  Form,
  Row,
  Col,
  DatePicker,
  Flex,
} from "antd";
import {
  FormOutlined,
  DeleteOutlined,
  ExclamationCircleFilled,
  EyeOutlined,
  PlusOutlined,
} from "@ant-design/icons";
import { useNavigate } from "react-router-dom";
import { GetListAll } from "../../API/Ingredient/ingredient";
import {
  GetAllUserPage,
  GetListEmployeePage,
  Post,
  GetByID,
  Remove,
} from "../../API/Employee/EmployeeAPI";
import { RegisterUser } from "../../API/UserManager/userManager";
import { Modal, Image, Select } from "antd";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { render } from "@testing-library/react";
const { Column, ColumnGroup } = Table;
const { Search } = Input;
const { confirm } = Modal;

const { RangePicker } = DatePicker;
const { TextArea } = Input;

const UserComponent = () => {
  const [nameSearch, SetNameSearch] = useState("");
  const columns = [
    {
      title: "Mã nhân viên",
      dataIndex: "EmployeeCode",
      width: "20%",
    },
    {
      title: "Tên nhân viên",
      dataIndex: "FullName",
      width: "40%",
    },

    {
      title: "Ảnh",
      dataIndex: "ImageLink",
      width: "20%",
      render: (_, record) => (
        <Image
          width={40}
          height={40}
          style={{ borderRadius: "50%" }}
          src={record.ImageLink}
        />
      ),
    },
    {
      title: "Tùy chọn",
      dataIndex: "action",
      width: "5%",
      render: (_, record) => (
        <Space size="middle">
          <a>
            <EyeOutlined />
          </a>
          <a onClick={() => DeleteAProject(record.key, record.EmployeeCode)}>
            <DeleteOutlined />
          </a>
        </Space>
      ),
    },
  ];
  const [form] = Form.useForm();
  const rules = {
    fullName: [{ required: true, message: "Tên người dùng không bỏ trống" }],
    userName: [{ required: true, message: "Tên tài khoản không bỏ trống" }],
  };
  const [totalPassengers, setTotalPassengers] = useState(1);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  const [data, SetData] = useState([]);
  const [isRender, SetIsRender] = useState(true);
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);
  const onSelectChange = (newSelectedRowKeys) => {
    console.log("selectedRowKeys changed: ", newSelectedRowKeys);
    setSelectedRowKeys(newSelectedRowKeys);
  };
  const rowSelection = {
    selectedRowKeys,
    onChange: onSelectChange,
  };
  useEffect(() => {
    if (isRender === true) fetchRecords(1, 10, nameSearch);
    SetIsRender(false);
  }, [isRender]);
  const fetchRecords = (pageNum, pageSize, nameSearch) => {
    setLoading(true);
    GetAllUserPage({ pageNum, pageSize, nameSearch }).then((res) => {
      console.log(res.data.value.totalPages);
      let dataShow = res.data.value.items.map((item) => {
        return {
          key: item.id,
          EmployeeCode: item.employeeCode,
          FullName: item.fullName,
          ImageLink: item.imageLink,
        };
      });
      SetData(dataShow);
      setTotalPassengers(res.data.value.totalPages);
      setLoading(false);
    });
  };
  const DeleteAProject = (id, name) => {
    confirm({
      title: "Bạn muốn xóa  " + name + " ?",
      icon: <ExclamationCircleFilled />,
      okText: "Có",
      okType: "danger",
      cancelText: "Quay lại",
      onOk() {
        Remove(id)
          .then((res) => {
            if (res.data.isSuccess === true) {
              SetIsRender(true);
              notify("Xóa user ");
            } else notifyError(res.data.message);
          })
          .catch((e) => {
            notifyError(e);
          });
      },
      onCancel() {},
    });
  };

  const [textTitle, SetTextTilte] = useState("");
  const [state, SetState] = useState("ADD");
  const [dataPush, SetDataPush] = useState({
    ID: "",
    Name: "",
    Measure: "",
    Description: "",
  });
  const [selectedItems, setSelectedItems] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const notify = (message) => {
    toast.success(message, {
      position: "top-right",
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "colored",
    });
  };
  const notifyError = (message) => {
    toast.error(message + "!", {
      position: "top-right",
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "colored",
    });
  };

  const showModal = (state, dataEdit) => {
    setIsModalOpen(true);
    SetState(state);
    console.log(dataEdit);
    if (state == "ADD") {
      SetTextTilte("Thêm mới nhân viên");
      ClearForm();
    }
  };
  const ClearForm = () => {
    SetDataPush({ fullName: "", userName: "", email: "", role: "User" });
  };

  const handleOk = () => {
    setIsModalOpen(false);
    if (state === "ADD") {
      RegisterUser(dataPush)
        .then((res) => {
          if (res.data.isSuccess === true) {
            SetIsRender(true);
            notify(
              "Thêm user thành công, mã nhân viên bạn là " + res.data.message
            );
            ClearForm();
            form.resetFields();
          } else notifyError(res.data.message);
        })
        .catch((e) => {
          notifyError(e);
        });
    }
    ClearForm();
  };
  const handleCancel = () => {
    setIsModalOpen(false);
    ClearForm();
    form.resetFields();
  };

  const handleChange = (e) => {
    SetDataPush((dataPush) => ({
      ...dataPush,
      [e.target.name]: e.target.value,
    }));
  };

  const onSearch = (value, _e, info) => {
    SetNameSearch(value);
    SetIsRender(true);
  };
  const handleChangeSelectModal = (value) => {
    console.log(value);
    SetDataPush({
      ...dataPush,
      role: value,
    });
  };
  return (
    <div style={{ padding: 10 }}>
      <ToastContainer />
      <div style={{ marginTop: "16px" }}>
        <div style={{ display: "flex", justifyContent: "space-between" }}>
          <div style={{ display: "flex" }}>
            <h3 style={{ marginRight: 20 }}>
              <FormOutlined
                style={{
                  strokeWidth: "30",
                  color: "blue",
                  stroke: "blue",
                  fontSize: 20,
                  fontWeight: 800,
                  marginRight: 8,
                }}
              ></FormOutlined>
              Danh sách thợ{" "}
            </h3>
            <Search
              placeholder="Nhập mã nhân viên"
              allowClear
              onSearch={onSearch}
              style={{
                width: 400,
                display: "flex",
                alignItems: "center",
              }}
            />
          </div>
          <div style={{ display: "flex", alignItems: "center" }}>
            <Button
              type="primary"
              onClick={() => showModal("ADD")}
              style={{ marginRight: 16 }}
              icon={<PlusOutlined />}
            >
              Thêm
            </Button>
            <Modal
              title={textTitle}
              open={isModalOpen}
              onOk={form.submit}
              onCancel={handleCancel}
            >
              <Form
                layout="horizontal"
                style={{ maxWidth: 800 }}
                form={form}
                onFinish={handleOk}
              >
                <Row>
                  <Col span={24}>
                    <Form.Item
                      name="fullName"
                      label="Họ và tên:"
                      rules={rules.fullName}
                    >
                      <Input
                        name="fullName"
                        value={dataPush.fullName}
                        onChange={handleChange}
                      />
                    </Form.Item>
                  </Col>
                </Row>
                <Row>
                  <Col span={24}>
                    <Form.Item
                      name="userName"
                      label="Tên tài khoản:"
                      rules={rules.userName}
                    >
                      <Input
                        name="userName"
                        value={dataPush.userName}
                        onChange={handleChange}
                      />
                    </Form.Item>
                  </Col>
                </Row>
                <Row>
                  <Col span={24}>
                    <Form.Item label="Quyền ">
                      <Select
                        defaultValue="User"
                        style={{ width: "100%" }}
                        onChange={handleChangeSelectModal}
                        options={[
                          { value: "User", label: "User" },
                          { value: "Employee", label: "Employee" },
                        ]}
                      />
                    </Form.Item>
                  </Col>
                </Row>
                <Row>
                  <Col span={24}>
                    <Form.Item label="Email">
                      <Input
                        name="email"
                        rows={4}
                        value={dataPush.email}
                        onChange={handleChange}
                      />
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

        <div>
          <span
            style={{
              marginLeft: 8,
            }}
          ></span>
        </div>
        <Table
          rowSelection={rowSelection}
          columns={columns}
          dataSource={data}
          pagination={{
            pageSize: 10,
            onChange: (page, pageSize) => {
              fetchRecords(page, pageSize, nameSearch);
            },
          }}
        ></Table>
      </div>
    </div>
  );
};

export default UserComponent;
