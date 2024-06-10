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
  InputNumber,
} from "antd";
import {
  LikeOutlined,
  DeleteOutlined,
  FormOutlined,
  EyeOutlined,
  PlusOutlined,
} from "@ant-design/icons";

import {
  GetListByPage,
  Remove,
  Post,
  Update,
} from "../../API/RegisterRemainningLongan/RegisterRemainningLonganAPI";
import { Modal, Image, Select } from "antd";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import dayjs from "dayjs";
import ConvertDate from "../../Common/Convert/ConvertDate";
const { Column, ColumnGroup } = Table;
const { Search } = Input;
const { RangePicker } = DatePicker;
const { TextArea } = Input;
const { confirm } = Modal;
const { Option } = Select;

const RegisterRemainningLonganComponent = () => {
  const [nameSearch, SetNameSearch] = useState("");
  const columns = [
    {
      title: "Ngày nhập",
      dataIndex: "created",
      width: "15%",
    },
    {
      title: "Mã nhân viên",
      dataIndex: "employeeCode",
      width: "10%",
    },

    {
      title: "Tên nhân viên",
      dataIndex: "employeeName",
      width: "20%",
    },
    {
      title: "Số khay",
      dataIndex: "amount",
      width: "20%",
    },
    {
      title: "Trạng thái",
      dataIndex: "status",
      width: "20%",
    },
    {
      title: "Tùy chọn",
      dataIndex: "action",
      width: "5%",
      render: (_, record) =>
        record.isCheck === 0 && (
          <Space size="middle">
            <a
              onClick={() =>
                Update({
                  id: record.key,
                  employeeID: record.employeeID,
                  isCheck: 1,
                }).then((res) => {
                  SetIsRender(true);
                  notify("Xác nhận");
                })
              }
            >
              <LikeOutlined style={{ color: "blue" }} />
            </a>
            <a
              onClick={() =>
                Update({
                  id: record.key,
                  employeeID: record.employeeID,
                  isCheck: 2,
                }).then((res) => {
                  SetIsRender(true);
                  notify("Hủy");
                })
              }
            >
              <DeleteOutlined style={{ color: "blue" }} />
            </a>
          </Space>
        ),
    },
  ];

  const [form] = Form.useForm();
  const rules = {
    amount: [{ required: true, message: "Số khay không bỏ trống" }],
  };
  const [totalPassengers, setTotalPassengers] = useState(1);
  const [loading, setLoading] = useState(false);
  const [data, SetData] = useState([]);
  const [isRender, SetIsRender] = useState(true);
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);
  const [listEmployee, SetListEmployee] = useState([]);
  const [resetData, SetResetData] = useState(true);

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
    if (isRender === true) fetchRecords(1, 10, nameSearch, dateSearch);
    SetIsRender(false);
  }, [isRender]);
  const fetchRecords = (pageNum, pageSize, nameSearch, dateSearch) => {
    setLoading(true);
    GetListByPage({ pageNum, pageSize, nameSearch, dateSearch }).then((res) => {
      let dataShow = res.data.value.items.map((item) => {
        return {
          key: item.id,
          employeeID: item.employeeID,
          employeeCode: item.employeeCode,
          employeeName: item.employeeName,
          amount: item.amount,
          isCheck: item.ischeck,
          created: ConvertDate(item.created),
          status: item.status,
        };
      });
      console.log(dataShow);
      SetData(dataShow);
      setTotalPassengers(res.data.value.totalPages);
      setLoading(false);
    });
  };

  const [textTitle, SetTextTilte] = useState("");
  const [state, SetState] = useState("ADD");
  const [dataPush, SetDataPush] = useState({
    id: "",
    amount: null,
    ischeck: 0,
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

  const showModal = () => {
    setIsModalOpen(true);
    SetTextTilte("Đăng kí trả nhãn");
    ClearForm();
  };

  const ClearForm = () => {
    SetDataPush({
      id: "",
      amount: null,
      ischeck: 0,
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
            ClearForm();
          } else notifyError("Không bỏ trống số khay");
        })
        .catch((e) => {
          notifyError("Không bỏ trống số khay");
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
      amount: e,
    }));
  };

  const onSearch = (value, _e, info) => {
    SetNameSearch(value);
    SetIsRender(true);
  };

  const [dateSearch, SetDateSearch] = useState(null);
  const onChangeDateSearch = (date, dateString) => {
    console.log(date, dateString);
    SetDateSearch(date);
    fetchRecords(1, 10, nameSearch, date);
  };
  const dataStatus = [
    { label: "Đã lập", value: 0 },
    { label: "Đã trả", value: 1 },
    { label: "Hủy bỏ", value: 2 },
  ];
  const handleChangeFilter = (value) => {
    SetNameSearch(value);
    SetIsRender(true);
  };
  return (
    <div>
      <ToastContainer />
      <div>
        <div style={{ display: "flex", justifyContent: "space-between" }}>
          <div style={{ display: "flex", alignItems: "center" }}>
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
              Danh sách đăng ký hoàn thành{" "}
            </h3>
            <Select
              style={{
                width: 200,
              }}
              allowClear
              placeholder="Trạng thái"
              onChange={handleChangeFilter}
            >
              {dataStatus.map((p) => (
                <Option value={p.value}>{p.label}</Option>
              ))}
            </Select>
          </div>
          <DatePicker
            placeholder="Chọn ngày"
            style={{ height: 34 }}
            onChange={onChangeDateSearch}
          />
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
              width={400}
              open={isModalOpen}
              onOk={form.submit}
              onCancel={handleCancel}
            >
              <Form layout="horizontal" form={form} onFinish={handleOk}>
                <Row>
                  <Col span={24}>
                    <Form.Item label="Số khay:" rules={rules.amount}>
                      <InputNumber
                        style={{ width: "100%" }}
                        value={dataPush.amount}
                        onChange={handleChange}
                      ></InputNumber>
                    </Form.Item>
                  </Col>
                </Row>
              </Form>
            </Modal>
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
              fetchRecords(page, pageSize, nameSearch, dateSearch);
            },
          }}
        ></Table>
      </div>
    </div>
  );
};

export default RegisterRemainningLonganComponent;
