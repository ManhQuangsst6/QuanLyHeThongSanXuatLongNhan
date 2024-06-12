import React, { useState, useEffect } from "react";
import {
  Space,
  Button,
  Table,
  InputNumber,
  Input,
  Form,
  Row,
  Col,
  DatePicker,
  Flex,
} from "antd";
import {
  LikeOutlined,
  DeleteOutlined,
  FileExcelOutlined,
  FormOutlined,
  PlusOutlined,
} from "@ant-design/icons";
import { useNavigate } from "react-router-dom";
import { GetListEmployeePage } from "../../API/Employee/EmployeeAPI";
import {
  GetListByPage,
  Remove,
  Post,
  Update,
  GetAllExportExcel,
  CreateTableSalary,
} from "../../API/Salary/SalaryAPI";
import { Modal, Image, Select } from "antd";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import dayjs from "dayjs";
import ConvertDate from "../../Common/Convert/ConvertDate";
import * as XLSX from "xlsx";
const { Column, ColumnGroup } = Table;
const { Search } = Input;
const { TextArea } = Input;
const { confirm } = Modal;
const { Option } = Select;

const SalaryComponent = () => {
  const [nameSearch, SetNameSearch] = useState("");
  const columns = [
    {
      title: "Quý",
      width: "10%",
      render: (_, record) => (
        <div>
          {record.quarterYear}/{record.year}
        </div>
      ),
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
      title: "Số cân",
      dataIndex: "sumAmount",
      width: "20%",
    },
    {
      title: "Thành tiền",
      dataIndex: "salaryMoney",
      width: "20%",
    },
    {
      title: "Trạng thái",
      dataIndex: "statusString",
      width: "15%",
    },
    {
      title: "Tùy chọn",
      dataIndex: "action",
      width: "5%",
      render: (_, record) => (
        <div></div>
        // <Space size="middle">
        //   <a
        //     onClick={() =>
        //       Update({
        //         id: record.key,
        //         employeeID: record.employeeID,
        //         isCheck: 1,
        //       }).then((res) => {
        //         SetIsRender(true);
        //         notify("Xác nhận");
        //       })
        //     }
        //   >
        //     <LikeOutlined style={{ color: "blue" }} />
        //   </a>
        //   <a
        //     onClick={() =>
        //       Update({
        //         id: record.key,
        //         employeeID: record.employeeID,
        //         isCheck: 3,
        //       }).then((res) => {
        //         SetIsRender(true);
        //         notify("Hủy");
        //       })
        //     }
        //   >
        //     <DeleteOutlined style={{ color: "blue" }} />
        //   </a>
        // </Space>
      ),
    },
  ];

  const [form] = Form.useForm();
  const rules = {
    quarterYear: [{ required: true, message: "Quý không bỏ trống" }],
    year: [{ required: true, message: "Năm không bỏ trống" }],
    price: [{ required: true, message: "tiền không bỏ trống" }],
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
  const [quarterYear, SetQuarterYear] = useState(null);
  const [year, SetYear] = useState(null);
  useEffect(() => {
    if (isRender === true) fetchRecords(1, 10, nameSearch, quarterYear, year);
    SetIsRender(false);
  }, [isRender]);
  const fetchRecords = (pageNum, pageSize, nameSearch, quarterYear, year) => {
    setLoading(true);
    GetListByPage({ pageNum, pageSize, nameSearch, quarterYear, year }).then(
      (res) => {
        let dataShow = res.data.value.items.map((item) => {
          return {
            key: item.id,
            employeeID: item.employeeID,
            employeeCode: item.employeeCode,
            employeeName: item.employeeName,
            sumAmount: item.sumAmount,
            quarterYear: item.quarterYear,
            year: item.year,
            salaryMoney: item.salaryMoney,
            status: item.status,
            statusString: item.statusString,
          };
        });
        console.log(dataShow);
        SetData(dataShow);
        setTotalPassengers(res.data.value.totalCount);
        setLoading(false);
      }
    );
  };

  const [textTitle, SetTextTilte] = useState("Tạo bảng lương");
  const [state, SetState] = useState("ADD");
  const [dataPush, SetDataPush] = useState({
    id: "",
    title: "",
    startDate: null,
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

  const showModal = () => {
    setIsModalOpen(true);
    ClearForm();
  };

  const ClearForm = () => {
    SetDataPush({
      quarterYear: null,
      price: null,
      year: null,
    });
  };

  const handleOk = () => {
    setIsModalOpen(false);
    if (state === "ADD") {
      CreateTableSalary(dataPush)
        .then((res) => {
          if (res.data.isSuccess === true) {
            SetIsRender(true);
            notify("Thêm ");
            form.resetFields();
            ClearForm();
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

  const handleChangeEmployee = (value) => {
    SetDataPush({
      ...dataPush,
      employeeID: value,
    });
  };
  const handleQuarterYear = (e) => {
    SetDataPush((dataPush) => ({
      ...dataPush,
      quarterYear: e,
    }));
  };
  const handleYear = (e) => {
    SetDataPush((dataPush) => ({
      ...dataPush,
      year: e,
    }));
  };
  const handlePrice = (e) => {
    SetDataPush((dataPush) => ({
      ...dataPush,
      price: e,
    }));
  };
  const [dateSearch, SetDateSearch] = useState(null);
  const onChangeDateSearch = (date, dateString) => {
    // console.log(date, dateString);
    // SetDateSearch(date)
    // fetchRecords(1, 10, nameSearch,date);
  };
  const dataStatus = [
    { label: "Đợi kiểm tra", value: 0 },
    { label: "Đang giao", value: 1 },
    { label: "Đã nhận", value: 2 },
    { label: "Hủy bỏ", value: 3 },
  ];
  const ExportExcel = () => {
    GetAllExportExcel(quarterYear, year).then((res) => {
      const result = res.data.value.map((item) => {
        return {
          Code: item.employeeCode,
          FullName: item.employeeName,
          Amount: item.sumAmount,
          Money: item.salaryMoney,
          Note: null,
          Check: null,
        };
      });
      const wb = XLSX.utils.book_new(),
        ws = XLSX.utils.json_to_sheet(result, { origin: "A3" });
      const header = [[`Bảng lương quý ${quarterYear} năm ${year}`]];
      const range = XLSX.utils.decode_range(ws["!ref"]);
      for (let C = range.s.c; C <= range.e.c; ++C) {
        ws[XLSX.utils.encode_cell({ c: C, r: 0 })] = { wch: 100 };
      }
      XLSX.utils.sheet_add_aoa(ws, header, { origin: "C1" });
      XLSX.utils.book_append_sheet(wb, ws, "Sheet1");
      XLSX.writeFile(wb, `Bảng_Lương_Quý${quarterYear}_năm${year}.xlsx`);
    });
  };
  const onChangequarterYear = (value) => {
    console.log("dsgkv");
    SetQuarterYear(value);
    SetIsRender(true);
  };
  const onChangeyear = (value) => {
    SetYear(value);
    SetIsRender(true);
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
              Bảng lương
            </h3>
            <InputNumber
              style={{ height: 32, margin: "auto" }}
              placeholder="Nhập quý"
              min={1}
              max={4}
              value={quarterYear}
              onChange={onChangequarterYear}
            />
            <InputNumber
              style={{ height: 32, margin: "auto", marginLeft: 12 }}
              placeholder="Nhập năm"
              min={1970}
              max={2050}
              value={year}
              onChange={onChangeyear}
            />
          </div>

          <div style={{ display: "flex", alignItems: "center" }}>
            <Button
              type="primary"
              style={{ marginRight: 16 }}
              icon={<PlusOutlined />}
              onClick={() => showModal("ADD")}
            >
              Thêm
            </Button>

            <Modal
              title={textTitle}
              open={isModalOpen}
              onOk={form.submit}
              onCancel={handleCancel}
            >
              <Form layout="horizontal" form={form} onFinish={handleOk}>
                <Row>
                  <Col span={24}>
                    <Form.Item
                      name="quarterYear"
                      label="Quý"
                      rules={rules.quarterYear}
                    >
                      <InputNumber
                        name="wholesalePrice"
                        style={{ width: "100%" }}
                        value={dataPush.quarterYear}
                        onChange={handleQuarterYear}
                      />
                    </Form.Item>
                  </Col>
                </Row>
                <Row>
                  <Col span={24}>
                    <Form.Item name="year" label="Năm: " rules={rules.year}>
                      <InputNumber
                        name="wholesalePrice"
                        style={{ width: "100%" }}
                        value={dataPush.year}
                        onChange={handleYear}
                      />
                    </Form.Item>
                  </Col>
                </Row>
                <Row>
                  <Col span={24}>
                    <Form.Item
                      name="price"
                      label="Giá một cân: "
                      rules={rules.price}
                    >
                      <InputNumber
                        name="price"
                        style={{ width: "100%" }}
                        value={dataPush.price}
                        onChange={handlePrice}
                      />
                    </Form.Item>
                  </Col>
                </Row>
              </Form>
            </Modal>
            <Button
              type="primary"
              onClick={() => ExportExcel()}
              danger
              icon={<FileExcelOutlined />}
            >
              Xuất file{" "}
            </Button>
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
          rowClassName={(record, index) =>
            index % 2 === 0 ? "table-row-light" : "table-row-dark"
          }
          rowSelection={rowSelection}
          columns={columns}
          dataSource={data}
          pagination={{
            total: totalPassengers,
            onChange: (page, pageSize) => {
              fetchRecords(page, pageSize, nameSearch, quarterYear, year);
            },
          }}
        ></Table>
      </div>
    </div>
  );
};

export default SalaryComponent;
