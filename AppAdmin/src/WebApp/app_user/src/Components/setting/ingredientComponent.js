import React, { useState, useEffect } from "react";
import { Space, Button, Table, Input, Form, Row, Col, DatePicker } from "antd";
import {
  EditOutlined,
  DeleteOutlined,
  ExclamationCircleFilled,
  EyeOutlined,
  PlusOutlined,
  FormOutlined,
} from "@ant-design/icons";
import { useNavigate } from "react-router-dom";
import {
  GetListAll,
  Post,
  Update,
  Remove,
} from "../../API/Ingredient/ingredient";
import { Modal } from "antd";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
const { Column, ColumnGroup } = Table;
const { Search } = Input;
const { confirm } = Modal;

const { RangePicker } = DatePicker;
const { TextArea } = Input;

const IngredientComponent = () => {
  const columns = [
    {
      title: "Tên",
      dataIndex: "Name",
      width: "20%",
    },
    {
      title: "Đơn vị",
      dataIndex: "Measure",
      width: "20%",
    },

    {
      title: "Description",
      dataIndex: "Description",
      width: "55%",
    },
    {
      title: "Tùy chọn",
      dataIndex: "action",
      width: "5%",
      render: (_, record) => (
        <Space size="middle">
          <a>
            <EyeOutlined style={{ color: "blue" }} />
          </a>
          <a onClick={() => showModal("EDIT", record)}>
            <EditOutlined style={{ color: "blue" }} />
          </a>
          <a onClick={() => DeleteIngredient(record.key, record.Name)}>
            <DeleteOutlined style={{ color: "blue" }} />
          </a>
        </Space>
      ),
    },
  ];
  const rules = {
    Name: [{ required: true, message: "Tên nguyên liệu không bỏ trống" }],
    Measure: [{ required: true, message: "Đơn vị không bỏ trống" }],
  };
  const [form] = Form.useForm();
  const navigate = useNavigate();
  const [data, SetData] = useState([]);
  const [isRender, SetIsRender] = useState(true);
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);

  useEffect(() => {
    if (isRender === true)
      GetListAll().then((res) => {
        console.log(res);
        let dataShow = res.data.value.map((item) => {
          return {
            key: item.id,
            Name: item.name,
            Measure: item.measure,
            Description: item.description,
          };
        });
        SetData(dataShow);
      });
    SetIsRender(false);
  }, [isRender]);
  const DeleteIngredient = (id, name) => {
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
              notify("Xóa nguyên liệu");
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
    console.log(dataEdit);
    if (state == "ADD") {
      SetTextTilte("Thêm mới nguyên liệu");
      ClearForm();
    } else if (state == "EDIT") SetTextTilte("Cập nhật nguyên liệu");
    if (dataEdit) {
      SetDataPush({
        ID: dataEdit.key,
        Name: dataEdit.Name,
        Measure: dataEdit.Measure,
        Description: dataEdit.Description,
      });
      form.setFieldValue("Name", dataEdit.Name);
      form.setFieldValue("Description", dataEdit.Description);
      form.setFieldValue("Measure", dataEdit.Measure);
    }
  };
  const ClearForm = () => {
    SetDataPush({ ID: "", Name: "", Measure: "", Description: "" });
  };
  const handleOk = () => {
    setIsModalOpen(false);
    if (state === "ADD") {
      console.log(dataPush);
      Post(dataPush)
        .then((res) => {
          if (res.data.isSuccess === true) {
            SetIsRender(true);
            notify("Thêm nguyên liệu");
            ClearForm();
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
            notify("Cập nhật nguyên liệu");
            ClearForm();
            form.resetFields();
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
            Danh sách nguyên liệu
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
                form={form}
                onFinish={handleOk}
                layout="horizontal"
                style={{ maxWidth: 800 }}
              >
                <Row>
                  <Col span={24}>
                    <Form.Item
                      name="Name"
                      label="Tên nguyên liệu:"
                      rules={rules.Name}
                    >
                      <Input
                        name="Name"
                        value={dataPush.Name}
                        onChange={handleChange}
                      />
                    </Form.Item>
                  </Col>
                </Row>
                <Row>
                  <Col span={24}>
                    <Form.Item
                      name="Measure"
                      label="Đơn vị đo: "
                      rules={rules.Measure}
                    >
                      <Input
                        name="Measure"
                        value={dataPush.Measure}
                        onChange={handleChange}
                      />
                    </Form.Item>
                  </Col>
                </Row>
                <Row>
                  <Col span={24}>
                    <Form.Item label="Mô tả:">
                      <TextArea
                        name="Description"
                        rows={4}
                        value={dataPush.Description}
                        onChange={handleChange}
                      />
                    </Form.Item>
                  </Col>
                </Row>
              </Form>
            </Modal>
          </div>
        </div>

        <div></div>
        <Table
          rowClassName={(record, index) =>
            index % 2 === 0 ? "table-row-light" : "table-row-dark"
          }
          columns={columns}
          dataSource={data}
          pagination={{
            pageSize: 10,
          }}
        ></Table>
      </div>
    </div>
  );
};

export default IngredientComponent;
