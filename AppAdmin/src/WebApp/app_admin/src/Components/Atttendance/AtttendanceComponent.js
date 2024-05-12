import React, { useState, useEffect } from 'react';
import { Space, Button, Table, Input,  DatePicker, InputNumber } from 'antd';
import {  PlusCircleOutlined, PlusOutlined } from '@ant-design/icons';
import { } from '../../API/Ingredient/ingredient';
import { GetListByPage,Update } from '../../API/Attendance/AttendanceAPI';
import { Modal } from 'antd';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
const { Search } = Input;
const { confirm } = Modal;




const { RangePicker } = DatePicker;
const { TextArea } = Input;


const AttendanceComponent = () => {
    const [inputValues, setInputValues] = useState({});

    const dateFormat = 'YYYY/MM/DD HH:mm:ss';
    const [nameSearch, SetNameSearch] = useState("")
    const columns = [
       
        {
            title: 'Mã nhân viên',
            dataIndex: 'EmployeeCode',
            width: '10%',
        },
        {
            title: 'Tên nhân viên',
            dataIndex: 'EmployeeName',
            width: '20%',
        },
        {
            title: 'Mã đã cân',
            dataIndex: 'ListAmount',
            width: '20%',
        },
        {
            title: 'Mã cân',
            width: '20%',
            render:(context,record)=>(
                <InputNumber
                value={inputValues[record.key]}
                 onChange={(value)=>ChangCurrentAmount(value,record)}
                  placeholder='Nhập mã cân'
                  ></InputNumber>
            )
        },
        {
            title: 'Tổng',
            dataIndex: 'SumAmount',
            width: '20%',
        },
        
        {
            title: 'Tùy chọn',
            dataIndex: 'action',
            width: '5%',
            render: (_, record) => (
                <Space size="middle" >
                    <a style={{fontSize:20}} onClick={() => UpdateAmount(record)} > <PlusCircleOutlined /></a>
                </Space>
            ),
        },

    ];
    const ChangCurrentAmount=(e,record)=>{
        const updatedInputValues = { ...inputValues, [record.key]: e };
  setInputValues(updatedInputValues);
       const obj= appendNumberToString(record.ListAmount,e)
       if(obj!==null ){
        const data=  {
            ID:record.key,
            EmployeeID:record.EmployeeID,
            ListAmount:obj.list,
            SumAmount:obj.sum
        }
        SetDataPush(data)
       }
        
    }
    const UpdateAmount=()=>{
        if(inputValues[dataPush.ID]==null||inputValues[dataPush.ID]=="") return;
      Update(dataPush).then(res=>{
        notify("Cập nhật ")
        SetDataPush({})
        setInputValues({});
        SetIsRender(true);
      })
    }
   
    const [totalPassengers, setTotalPassengers] = useState(1);
    const [loading, setLoading] = useState(false);
    const [data, SetData] = useState([]);
    const [isRender, SetIsRender] = useState(true);
    const [listEmployee, SetListEmployee] = useState([])
    const [listCategory, SetListCategory] = useState([])
    const [resetData, SetResetData] = useState(true)
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
            fetchRecords(1, 10, nameSearch,dateSearch)
        SetIsRender(false)
    }, [isRender])
    const fetchRecords = (pageNum, pageSize, nameSearch,dateSearch) => {
        setLoading(true);
        GetListByPage({ pageNum, pageSize, nameSearch,dateSearch })
            .then((res) => {
                console.log(res.data.value.items)
                let dataShow = res.data.value.items.map(item => {
                    return {
                        key: item.id,
                        EmployeeID:item.employeeID,
                        EmployeeCode: item.employeeCode,
                        EmployeeName: item.employeeName,
                        ListAmount: item.listAmount,
                        CurrentAmount:null,
                        SumAmount: item.sumAmount
                    }
                })
                SetData(dataShow)
                setTotalPassengers(res.data.value.totalPages);
                setLoading(false);
            });
    };

    const [textTitle, SetTextTilte] = useState("")
    const [state, SetState] = useState("ADD")
    const [dataPush, SetDataPush] = useState({})

    const [selectedItems, setSelectedItems] = useState([]);
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
        
    };
   
    const handleOk = () => {
        
    };

    const onSearch = (value, _e, info) => {
        SetNameSearch(value);
        SetIsRender(true)
    }
    const ChangeRangePicker = (listValue) => {
        dataPush.dateFrom = listValue[0]
        dataPush.dateTo = listValue[1]
        SetDataRangDate(listValue)
    }
   
    const [DataRangDate, SetDataRangDate] = useState([]);
    const [dateSearch,SetDateSearch]=useState(null)
    const onChangeDateSearch = (date, dateString) => {
        console.log(date, dateString);
        SetDateSearch(date)
        fetchRecords(1, 10, nameSearch,date);
      };
      const appendNumberToString=(inputString, numberToAdd)=> {
        if(numberToAdd==null || numberToAdd=="") 
            return null;
        if(inputString=="" || inputString==null) 
            return  {list:numberToAdd.toString(), sum:numberToAdd};
        const numbers = inputString.split(',');
        numbers.push(numberToAdd);
        const sum = numbers.reduce((accumulator, currentValue) => accumulator + parseInt(currentValue), 0);
        const resultString = numbers.join(',');
        return {list:resultString, sum:sum};
    }
    return (

        <div style={{ padding: 10 }}>
            <ToastContainer />
            <div style={{ marginTop: "16px", }}>
                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                   <div style={{display:'flex'}}> 
                    <h3 style={{marginRight:20,lineHeight:"55px"}}>Danh sách nhân viên </h3>
                    <Search
                    placeholder="Nhập mã nhân viên"
                    allowClear
                    onSearch={onSearch}
                    style={{
                        width:400,
                       display:'flex',alignItems:'center'
                    }}
                />
                <DatePicker placeholder='Chọn ngày' style={{height:34,margin:27}} onChange={onChangeDateSearch} />
                </div>
                    <div style={{ display: 'flex', alignItems: 'center' }} >

                        <Button type="primary" onClick={() => showModal("ADD")} style={{ marginRight: 16 }}
                         icon={<PlusOutlined />}>
                            Thêm
                        </Button>
                       
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
                            fetchRecords(page, pageSize, nameSearch,dateSearch);
                        }
                    }}
                >


                </Table>
            </div>
        </div>

    )
}

export default AttendanceComponent;