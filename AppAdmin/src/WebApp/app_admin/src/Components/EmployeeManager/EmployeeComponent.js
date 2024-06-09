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

const { RangePicker } = DatePicker;
const { TextArea } = Input;
const { confirm } = Modal;

const EmployeeeComponent = () => {
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
          <a onClick={() => DeleteAProject(record.key, record.name)}>
            <DeleteOutlined />
          </a>
        </Space>
      ),
    },
  ];
  const rules = {
    fullName: [{ required: true, message: "Tên người dùng không bỏ trống" }],
    userName: [{ required: true, message: "Tên tài khoản không bỏ trống" }],
  };
  const [form] = Form.useForm();
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
  const hasSelected = selectedRowKeys.length > 0;
  useEffect(() => {
    if (isRender === true) fetchRecords(1, 1, nameSearch);
    SetIsRender(false);
  }, [isRender]);
  const fetchRecords = () => {
    GetListEmployeePage().then((res) => {
      let dataShow = res.data.value.map((item) => {
        return {
          key: item.id,
          EmployeeCode: item.employeeCode,
          FullName: item.fullName,
          ImageLink: item.imageLink,
        };
      });
      SetData(dataShow);
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
    SetDataPush({ fullName: "", userName: "", email: "", role: "Employee" });
  };

  const handleOk = () => {
    setIsModalOpen(false);
    if (state === "ADD") {
      RegisterUser(dataPush)
        .then((res) => {
          if (res.data.isSuccess === true) {
            SetIsRender(true);
            ClearForm();
            form.resetFields();
            notify(
              "Thêm nhân viên thành công, mã nhân viên bạn là " +
                res.data.message
            );
          } else notifyError(res.data.message);
        })
        .catch((e) => {
          notifyError(e);
        });
    }
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
    <div>
      <ToastContainer />
      <div>
        <div style={{ display: "flex", justifyContent: "space-between" }}>
          <h3>
            <FormOutlined
              style={{
                strokeWidth: "100",
                color: "blue",
                stroke: "blue",
                fontSize: 20,
                fontWeight: 800,
                marginRight: 8,
              }}
            />
            Danh sách nhân viên{" "}
          </h3>
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
                        defaultValue="Employee"
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
            <Button>Xóa </Button>
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
        ></Table>
      </div>
    </div>
  );
};

export default EmployeeeComponent;
