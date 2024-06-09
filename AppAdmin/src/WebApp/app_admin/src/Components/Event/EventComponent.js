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
  EditOutlined,
  DeleteOutlined,
  ExclamationCircleFilled,
  EyeOutlined,
  PlusOutlined,
} from "@ant-design/icons";
import { useNavigate } from "react-router-dom";
import { GetListEmployeePage } from "../../API/Employee/EmployeeAPI";
import {
  GetListEventsByPage,
  GetByID,
  Post,
  Update,
  Remove,
} from "../../API/Event/EventAPI";
import { Modal, Image, Select } from "antd";
import { ToastContainer, toast } from "react-toastify";
import { Typography } from "antd";
import "react-toastify/dist/ReactToastify.css";
import { render } from "@testing-library/react";
import dayjs from "dayjs";
import ConvertDate from "../../Common/Convert/ConvertDate";
const { Column, ColumnGroup } = Table;
const { Search } = Input;

const { RangePicker } = DatePicker;
const { TextArea } = Input;
const { confirm } = Modal;

const EventComponent = () => {
  const [nameSearch, SetNameSearch] = useState("");
  const columns = [
    {
      title: "Tên sự kiện",
      dataIndex: "title",
      width: "35%",
    },
    {
      title: "Mã nhân viên",
      dataIndex: "employeeCode",
      width: "20%",
    },
    {
      title: "Ngày nhập",
      dataIndex: "dateStart",
      width: "20%",
    },
    {
      title: "Tiền chi ",
      dataIndex: "expense",
      width: "20%",
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
          <a onClick={() => showModal("EDIT", record)}>
            <EditOutlined />
          </a>
          <a onClick={() => DeletePurcharse(record.key)}>
            <DeleteOutlined />
          </a>
        </Space>
      ),
    },
  ];
  const [form] = Form.useForm();
  const rules = {
    title: [{ required: true, message: "Tiêu đề không bỏ trống" }],
    dateStart: [{ required: true, message: "Ngày không bỏ trống" }],
    expense: [{ required: true, message: "Chi tiêu không bỏ trống" }],
    employeeID: [{ required: true, message: "Tên nhân viên không bỏ trống" }],
  };
  const [totalPassengers, setTotalPassengers] = useState(1);
  const [loading, setLoading] = useState(false);
  const [data, SetData] = useState([]);
  const [isRender, SetIsRender] = useState(true);
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);
  const [listEmployee, SetListEmployee] = useState([]);
  const [resetData, SetResetData] = useState(true);
  useEffect(() => {
    if (resetData) {
      GetListEmployeePage().then((res) => {
        let data = res.data.value.map((item) => {
          return {
            value: item.id,
            label: item.employeeCode,
          };
        });
        SetListEmployee(data);
      });
    }
  }, [resetData]);
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
    if (isRender === true) fetchRecords(1, 10, nameSearch);
    SetIsRender(false);
  }, [isRender]);
  const fetchRecords = (pageNum, pageSize, nameSearch) => {
    setLoading(true);
    GetListEventsByPage({ pageNum, pageSize, nameSearch }).then((res) => {
      console.log(res.data.value.items);
      let dataShow = res.data.value.items.map((item) => {
        return {
          key: item.id,
          title: item.title,
          dateStart: ConvertDate(item.dateStart),
          expense: item.expense,
          employeeCode: item.employeeCode,
        };
      });
      SetData(dataShow);
      setTotalPassengers(res.data.value.totalPages);
      setLoading(false);
    });
  };
  const DeletePurcharse = (id) => {
    confirm({
      title: "Bạn có muốn xóa nhập hàng ?",
      icon: <ExclamationCircleFilled />,
      okText: "Có",
      okType: "danger",
      cancelText: "Quay lại",
      onOk() {
        Remove(id)
          .then((res) => {
            if (res.data.isSuccess === true) {
              SetIsRender(true);
              notify("Xóa đơn nhập");
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
    id: "",
    title: "",
    dateStart: "",
    expense: null,
    employeeID: "",
    description: "",
  });
  const [selectedItems, setSelectedItems] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const notify = (message) => {
    toast.success(message + " thành công!", {
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
    if (state == "ADD") {
      SetTextTilte("Thêm mới sự kiện");
      ClearForm();
    } else if (state == "EDIT") SetTextTilte("Cập nhật nhật sự kiện");
    if (dataEdit) {
      GetByID(dataEdit.key).then((res) => {
        SetDataPush({
          ...dataPush,
          ...res.data.value,
          dateStart: dayjs(res.data.value.dateStart),
        });
        form.setFieldValue("title", res.data.value.title);
        form.setFieldValue("employeeID", res.data.value.employeeID);
        form.setFieldValue("expense", res.data.value.expense);
        form.setFieldValue("dateStart", dayjs(res.data.value.dateStart));
      });
    }
  };

  const ClearForm = () => {
    SetDataPush({
      id: "",
      title: "",
      dateStart: "",
      expense: null,
      employeeID: "",
      description: "",
    });
  };

  const handleOk = () => {
    setIsModalOpen(false);
    if (state === "ADD") {
      console.log(dataPush);
      Post(dataPush)
        .then((res) => {
          if (res.data.isSuccess === true) {
            SetIsRender(true);
            notify("Thêm ");
            form.resetFields();
          } else notifyError(res.data.message);
        })
        .catch((e) => {
          notifyError(e);
        });
    } else {
      Update(dataPush)
        .then((res) => {
          if (res.data.isSuccess === true) {
            SetIsRender(true);
            notify("Cập nhật ");
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

  const handleChangeEmployee = (value) => {
    SetDataPush({
      ...dataPush,
      employeeID: value,
    });
  };

  const onChangeDatePicker = (date, dateString) => {
    console.log(date);
    SetDataPush({
      ...dataPush,
      dateStart: date,
    });
  };
  return (
    <div>
      <ToastContainer />
      <div>
        <div style={{ display: "flex", justifyContent: "space-between" }}>
          <div style={{ display: "flex" }}>
            <h3 style={{ marginRight: 20 }}>
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
              Danh sách sự kiện{" "}
            </h3>
            <Search
              placeholder="Nhập tên sự kiện"
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
              width={800}
              open={isModalOpen}
              onOk={form.submit}
              onCancel={handleCancel}
            >
              <Form layout="horizontal" form={form} onFinish={handleOk}>
                <Row>
                  <Col span={12}>
                    <Form.Item
                      name="employeeID"
                      label="Nhân viên:"
                      rules={rules.employeeID}
                    >
                      <Select
                        value={dataPush.employeeID}
                        onChange={handleChangeEmployee}
                        options={listEmployee}
                      />
                    </Form.Item>
                  </Col>
                  <Col span={1}></Col>
                  <Col span={11}>
                    <Form.Item
                      name="title"
                      label="Nguyên tiêu đề:"
                      rules={rules.title}
                    >
                      <Input
                        name="title"
                        rows={4}
                        value={dataPush.title}
                        onChange={handleChange}
                      ></Input>
                    </Form.Item>
                  </Col>
                </Row>
                <Row>
                  <Col span={12}>
                    <Form.Item
                      name="expense"
                      label="Tiền chi:"
                      rules={rules.expense}
                    >
                      <Input
                        name="expense"
                        rows={4}
                        value={dataPush.expense}
                        onChange={handleChange}
                      ></Input>
                    </Form.Item>
                  </Col>
                  <Col span={1}></Col>
                  <Col span={11}>
                    <Form.Item
                      name="dateStart"
                      label="Ngày nhập:"
                      rules={rules.dateStart}
                    >
                      <DatePicker
                        value={dataPush.dateStart}
                        style={{ width: "100%" }}
                        onChange={onChangeDatePicker}
                      ></DatePicker>
                    </Form.Item>
                  </Col>
                </Row>

                <Row>
                  <Col span={24}>
                    <Form.Item label="ghi chú:">
                      <TextArea
                        name="description"
                        rows={4}
                        value={dataPush.description}
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
          >
            {hasSelected ? `Selected ${selectedRowKeys.length} items` : ""}
          </span>
        </div>
        <Table
          rowSelection={rowSelection}
          columns={columns}
          dataSource={data}
          pagination={{
            pageSize: 10,
            total: totalPassengers,
            onChange: (page, pageSize) => {
              fetchRecords(page, pageSize, nameSearch);
            },
          }}
        ></Table>
      </div>
    </div>
  );
};

export default EventComponent;
