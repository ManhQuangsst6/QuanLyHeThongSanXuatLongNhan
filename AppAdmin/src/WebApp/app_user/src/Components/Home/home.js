import React from 'react';
import { Card, Col, Row } from 'antd';
import { Line,Doughnut } from 'react-chartjs-2';
import { Chart, CategoryScale,ArcElement,registerables } from "chart.js";

function Home() {
  Chart.register(CategoryScale);
  Chart.register(ArcElement);
  Chart.register(...registerables);
    return (
      <div>
        <Row style={{display:'flex',justifyContent:'center',marginBottom:50}}>
          <Col span={5}>
            <Card
              style={{
             backgroundColor:'blue', color:'#fff', fontWeight:700,
              fontSize:20
                 }}
                >
               <div style={{float:'right'}}>
                  <div style={{}} ><div style={{fontSize:30,float:'right'}}>50</div></div>
                  <div style={{clear:'both'}}></div>
                  <div style={{}}>Nhân viên</div>
                </div>
    

            </Card>
          </Col>
          <Col span={1}> </Col>
          <Col span={5}>
          <Card
              style={{
              backgroundColor:'red', color:'#fff', fontWeight:700,
              fontSize:20
                 }}
                >
               <div style={{float:'right'}}>
                  <div style={{}} ><div style={{fontSize:30,float:'right'}}>122</div></div>
                  <div style={{clear:'both'}}></div>
                  <div style={{}}>Lô hàng</div>
                </div>
    

            </Card>
          </Col>
          <Col span={1}> </Col>
          <Col span={5}>
          <Card
              style={{
             backgroundColor:'#008038ad', color:'#fff', fontWeight:700,
              fontSize:20
                 }}
                >
               <div style={{float:'right'}}>
                  <div style={{}} ><div style={{fontSize:30,float:'right'}}>12450</div></div>
                  <div style={{clear:'both'}}></div>
                  <div style={{}}>Sản phẩm(kg)</div>
                </div>
    

            </Card>
          </Col>
          <Col span={1}> </Col>
           <Col span={5}>
           <Card
              style={{
             backgroundColor:'#660080ad', color:'#fff', fontWeight:700,
              fontSize:20
                 }}
                >
               <div style={{float:'right'}}>
                  <div style={{}} ><div style={{fontSize:30,float:'right'}}>123</div></div>
                  <div style={{clear:'both'}}></div>
                  <div style={{}}>Đã bán(kg) </div>
                </div>
    

            </Card>
          </Col>
        </Row>
     
<div style={{display:'flex', justifyContent:"space-around",width:"600px",alignItems:'end'}}>
<Doughnut style={{flex:1,width:400, height:400}} 
        data={{
          labels: [
            "Loai 1",
            "Loai 2",
            "Loai 3",
          ],
          datasets: [
            {
              label: "Population (millions)",
              backgroundColor: [
                "#3e95cd",
                "#8e5ea2",
                "#3cba9f"
              ],
              data: [2478, 5267, 734]
            }
          ]
        }}
        
        
      />
       <Line style={{flex:1,width:'40%',marginLeft:160}} 
        data={{
        labels: [2021, 2022, 2023, 2024],
        datasets: [
        {
          data: [86, 114, 106, 106],
          label: "Loai 1",
          borderColor: "#3e95cd",
          fill: false
        },
      ]
    }}
  />
</div>
<Row>
  <Col style={{fontSize:20,fontWeight:600}} span={10}>Biểu đồ tỉ lệ sản xuất giữa các sản phẩm </Col>
  <Col style={{fontSize:20,fontWeight:600}} span={12}>Biểu đồ sản lượng sản xuất qua các năm </Col>
</Row>
</div>
        )
}

export default Home;
