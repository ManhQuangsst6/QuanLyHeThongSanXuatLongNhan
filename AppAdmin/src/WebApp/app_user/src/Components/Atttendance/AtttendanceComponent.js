import React, { useState, useEffect } from "react";
import { Space, Button, Table, Input, DatePicker, InputNumber } from "antd";
import {
  FormOutlined,
  CloseOutlined,
  ExclamationCircleFilled,
  CheckOutlined,
} from "@ant-design/icons";
import {} from "../../API/Ingredient/ingredient";
import {
  GetListByUser,
  ComfirmByEmployee,
} from "../../API/Attendance/AttendanceAPI";
import { Modal } from "antd";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import ConvertDate from "../../Common/Convert/ConvertDate";
import NotificationComponent from "../Notification/NotificationComponent";
const { Search } = Input;
const { confirm } = Modal;

const { RangePicker } = DatePicker;
const { TextArea } = Input;

const AttendanceComponent = () => {
  const [inputValues, setInputValues] = useState({});

  const dateFormat = "YYYY/MM/DD HH:mm:ss";
  const [nameSearch, SetNameSearch] = useState("");
  const columns = [
    {
      title: "Ngày",
      dataIndex: "Created",
      width: "20%",
    },
    {
      title: "Mã đã cân",
      dataIndex: "ListAmount",
      width: "20%",
    },
    {
      title: "Tổng",
      dataIndex: "SumAmount",
      width: "20%",
    },
    {
      title: "Trạng thái",
      dataIndex: "ComfirmAmount",
      width: "20%",
      render: (_, record) =>
        record.ComfirmAmount === 1 ? (
          <CheckOutlined
            style={{ stroke: "green", color: "green", strokeWidth: 200 }}
          />
        ) : (
          <CloseOutlined
            style={{ stroke: "red", color: "red", strokeWidth: 200 }}
          />
        ),
      align: "center",
    },
    {
      title: "Tùy chọn",
      dataIndex: "action",
      width: "5%",
      align: "center",
      render: (_, record) =>
        record.ComfirmAmount < 1 && (
          <Space
            align="baseline"
            style={{ display: "flex", justifyContent: "center" }}
          >
            <a
              style={{
                fontSize: 20,
                stroke: "red",
                color: "red",
                strokeWidth: 200,
              }}
              onClick={() => ComfirmData(record)}
            >
              <CloseOutlined color="red" />
            </a>
          </Space>
        ),
    },
  ];
  const ChangCurrentAmount = (e, record) => {
    const updatedInputValues = { ...inputValues, [record.key]: e };
    setInputValues(updatedInputValues);
    const obj = appendNumberToString(record.ListAmount, e);
    if (obj !== null) {
      const data = {
        ID: record.key,
        EmployeeID: record.EmployeeID,
        ListAmount: obj.list,
        SumAmount: obj.sum,
      };
      SetDataPush(data);
    }
  };
  const ComfirmData = (data) => {
    confirm({
      title: "Số cân ngày " + data.Created + " có sự nhầm lẫn " + "?",
      icon: <ExclamationCircleFilled />,
      okText: "Xác nhận",
      okType: "danger",
      cancelText: "Quay lại",
      onOk() {
        ComfirmByEmployee(data.key)
          .then((res) => {
            if (res.data.isSuccess === true) {
              SetIsRender(true);
              notify("Gửi yêu cầu ");
            } else notifyError(res.data.message);
          })
          .catch((e) => {
            notifyError(e);
          });
      },
      onCancel() {},
    });
  };
  const [totalPassengers, setTotalPassengers] = useState(1);
  const [loading, setLoading] = useState(false);
  const [data, SetData] = useState([]);
  const [isRender, SetIsRender] = useState(true);
  const [listEmployee, SetListEmployee] = useState([]);
  const [listCategory, SetListCategory] = useState([]);
  const [resetData, SetResetData] = useState(true);
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
    if (isRender === true) fetchRecords(1, 10, nameSearch, dateSearch);
    SetIsRender(false);
  }, [isRender]);
  const fetchRecords = (pageNum, pageSize, nameSearch, dateSearch) => {
    setLoading(true);
    GetListByUser({ pageNum, pageSize, nameSearch, dateSearch }).then((res) => {
      console.log(res.data.value.items);
      let dataShow = res.data.value.items.map((item) => {
        return {
          key: item.id,
          EmployeeID: item.employeeID,
          EmployeeCode: item.employeeCode,
          EmployeeName: item.employeeName,
          ListAmount: item.listAmount,
          CurrentAmount: null,
          SumAmount: item.sumAmount,
          ComfirmAmount: item.comfirmAmount,
          Created: ConvertDate(item.created),
        };
      });
      SetData(dataShow);
      setTotalPassengers(res.data.value.totalPages);
      setLoading(false);
    });
  };

  const [textTitle, SetTextTilte] = useState("");
  const [state, SetState] = useState("ADD");
  const [dataPush, SetDataPush] = useState({});

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

  const showModal = (state, dataEdit) => {};

  const handleOk = () => {};

  const onSearch = (value, _e, info) => {
    SetNameSearch(value);
    SetIsRender(true);
  };
  const ChangeRangePicker = (listValue) => {
    dataPush.dateFrom = listValue[0];
    dataPush.dateTo = listValue[1];
    SetDataRangDate(listValue);
  };

  const [DataRangDate, SetDataRangDate] = useState([]);
  const [dateSearch, SetDateSearch] = useState(null);
  const onChangeDateSearch = (date, dateString) => {
    console.log(date, dateString);
    SetDateSearch(date);
    fetchRecords(1, 10, nameSearch, date);
  };
  const appendNumberToString = (inputString, numberToAdd) => {
    if (numberToAdd == null || numberToAdd == "") return null;
    if (inputString == "" || inputString == null)
      return { list: numberToAdd.toString(), sum: numberToAdd };
    const numbers = inputString.split(",");
    numbers.push(numberToAdd);
    const sum = numbers.reduce(
      (accumulator, currentValue) => accumulator + parseInt(currentValue),
      0
    );
    const resultString = numbers.join(",");
    return { list: resultString, sum: sum };
  };
  return (
    <div>
      <ToastContainer />
      <div>
        <div
          style={{
            display: "flex",
            justifyContent: "space-between",
            flexWrap: "wrap",
          }}
        >
          <h3 style={{ margin: 0 }}>
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
            Bảng công
          </h3>
          <DatePicker
            placeholder="Từ"
            style={{ height: 34 }}
            onChange={onChangeDateSearch}
          />
        </div>
        <Table
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

export default AttendanceComponent;
