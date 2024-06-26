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
  InputNumber,
} from "antd";
import {
  EditOutlined,
  DeleteOutlined,
  CaretRightOutlined,
  FormOutlined,
  ExclamationCircleFilled,
  PlusOutlined,
} from "@ant-design/icons";
import { useNavigate } from "react-router-dom";
import {
  GetListAll,
  Post,
  Update,
  Remove,
} from "../../API/Category/categoryAPI";
import { Modal } from "antd";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
const { Column, ColumnGroup } = Table;
const { Search } = Input;
const { confirm } = Modal;

const { RangePicker } = DatePicker;
const { TextArea } = Input;

const CategoryComponent = () => {
  const [formErrors, setFormErrors] = useState({});
  const columns = [
    {
      title: "Tên",
      dataIndex: "name",
      width: "20%",
    },
    {
      title: "Giá bán sỉ",
      dataIndex: "wholesalePrice",
      width: "10%",
    },
    {
      title: "Giá bán lẻ",
      dataIndex: "retailPrice",
      width: "10%",
    },

    {
      title: "Description",
      dataIndex: "description",
      width: "40%",
    },
    {
      title: "Tùy chọn",
      dataIndex: "action",
      width: "5%",
      render: (_, record) => (
        <Space size="middle">
          <a onClick={() => showModal("EDIT", record)}>
            <EditOutlined style={{ color: "blue" }} />
          </a>
          <a onClick={() => DeleteCategory(record.key, record.name)}>
            <DeleteOutlined style={{ color: "blue" }} />
          </a>
        </Space>
      ),
    },
  ];
  const navigate = useNavigate();
  const [data, SetData] = useState([]);
  const [isRender, SetIsRender] = useState(true);

  useEffect(() => {
    if (isRender === true)
      GetListAll().then((res) => {
        console.log(res);
        let dataShow = res.data.value.map((item) => {
          return {
            key: item.id,
            name: item.name,
            wholesalePrice: item.wholesalePrice,
            retailPrice: item.retailPrice,
            description: item.description,
          };
        });
        SetData(dataShow);
      });
    SetIsRender(false);
  }, [isRender]);
  const DeleteCategory = (id, name) => {
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
              notify("Xóa Loại long nhãn");
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
    name: "",
    description: "",
    wholesalePrice: 0,
    retailPrice: 0,
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
    SetState(state);
    console.log(dataEdit);
    if (state == "ADD") {
      SetTextTilte("Thêm mới loại long nhãn");
      ClearForm();
    } else if (state == "EDIT") SetTextTilte("Cập nhật loại long nhãn");
    if (dataEdit) {
      SetDataPush({
        id: dataEdit.key,
        name: dataEdit.name,
        description: dataEdit.description,
        wholesalePrice: dataEdit.wholesalePrice,
        retailPrice: dataEdit.retailPrice,
      });
      form.setFieldValue("name", dataEdit.name);
      form.setFieldValue("description", dataEdit.description);
      form.setFieldValue("wholesalePrice", dataEdit.wholesalePrice);
      form.setFieldValue("retailPrice", dataEdit.retailPrice);
    }

    console.log(dataPush);
    setIsModalOpen(true);
  };
  const ClearForm = () => {
    SetDataPush({
      id: "",
      name: "",
      description: "",
      wholesalePrice: null,
      retailPrice: null,
    });
  };

  const handleOk = () => {
    console.log(dataPush);
    setIsModalOpen(false);
    if (state === "ADD") {
      console.log(dataPush);
      Post(dataPush)
        .then((res) => {
          if (res.data.isSuccess === true) {
            SetIsRender(true);
            notify("Thêm loại long nhãn");
            form.resetFields();
            ClearForm();
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
            notify("Cập nhật loại long nhãn ");
            form.resetFields();
            ClearForm();
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
  const handleChangeRetailPrice = (e) => {
    SetDataPush((dataPush) => ({
      ...dataPush,
      retailPrice: e,
    }));
  };
  const handleChangeWholesalePrice = (e) => {
    SetDataPush((dataPush) => ({
      ...dataPush,
      wholesalePrice: e,
    }));
    form.setFieldValue("");
  };
  const rules = {
    name: [{ required: true, message: "Tên loại không bỏ trống" }],
    wholesalePrice: [{ required: true, message: "Giá sỉ không bỏ trống" }],
    retailPrice: [{ required: true, message: "Giá bán lẻ không bỏ trống" }],
  };
  const [form] = Form.useForm();
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
            Danh sách loại nhãn
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
              <Form layout="horizontal" form={form} onFinish={handleOk}>
                <Row>
                  <Col span={24}>
                    <Form.Item name="name" label="Tên loại:" rules={rules.name}>
                      <Input
                        name="name"
                        value={dataPush.name}
                        onChange={handleChange}
                      />
                    </Form.Item>
                  </Col>
                </Row>
                <Row>
                  <Col span={24}>
                    <Form.Item
                      name="wholesalePrice"
                      label="Giá bán lẻ: "
                      rules={rules.wholesalePrice}
                    >
                      <InputNumber
                        name="wholesalePrice"
                        style={{ width: "100%" }}
                        value={dataPush.wholesalePrice}
                        onChange={handleChangeWholesalePrice}
                      />
                    </Form.Item>
                  </Col>
                </Row>
                <Row>
                  <Col span={24}>
                    <Form.Item
                      name="retailPrice"
                      label="Giá bán buôn: "
                      rules={rules.retailPrice}
                    >
                      <InputNumber
                        name="retailPrice"
                        style={{ width: "100%" }}
                        value={dataPush.retailPrice}
                        onChange={handleChangeRetailPrice}
                      />
                    </Form.Item>
                  </Col>
                </Row>
                <Row>
                  <Col span={24}>
                    <Form.Item label="Mô tả:" name="description">
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
          </div>
        </div>
        <Table
          rowClassName={(record, index) =>
            index % 2 === 0 ? "table-row-light" : "table-row-dark"
          }
          columns={columns}
          dataSource={data}
        ></Table>
      </div>
    </div>
  );
};

export default CategoryComponent;
