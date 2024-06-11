import React, { useEffect, useState } from "react";
import { Card, Col, Row } from "antd";
import { Line, Doughnut } from "react-chartjs-2";
import { Chart, CategoryScale, ArcElement, registerables } from "chart.js";
import {
  GetCountEmployye,
  GetCountShipment,
  GetCountLogan,
  GetLonganCommon,
  GetLoganByCategory,
} from "../../API/Home/homeAPI";
import Item from "antd/es/list/Item";
function Home() {
  Chart.register(CategoryScale);
  Chart.register(ArcElement);
  Chart.register(...registerables);
  const [countEmployee, SetCountEmployee] = useState(null);
  const [countShipment, SetCountShipment] = useState(null);
  const [countLongan, SetCountLongan] = useState(null);
  const [longanDatasYear, SetlonganDatasYear] = useState([]);
  const [longanDatas, SetlonganDatas] = useState([]);
  useEffect(() => {
    GetCountEmployye().then((res) => {
      SetCountEmployee(res.data);
    });
    GetCountShipment().then((res) => {
      SetCountShipment(res.data);
    });
    GetCountLogan().then((res) => {
      SetCountLongan(res.data);
    });
    GetLonganCommon().then((res) => {
      SetlonganDatasYear(res.data);
    });
    GetLoganByCategory().then((res) => {
      SetlonganDatas(res.data);
    });
  }, []);
  const colors = [
    "#3e95cd",
    "#8e5ea2",
    "#3cba9f",
    "#e8c3b9",
    "#c45850",
    "#ff6384",
    "#36a2eb",
    "#cc65fe",
    "#ffce56",
    "#4bc0c0",
  ];
  return (
    <div>
      <Row
        style={{ display: "flex", justifyContent: "center", marginBottom: 50 }}
      >
        <Col span={5}>
          <Card
            style={{
              backgroundColor: "blue",
              color: "#fff",
              fontWeight: 700,
              fontSize: 20,
            }}
          >
            <div style={{ float: "right" }}>
              <div style={{}}>
                <div style={{ fontSize: 30, float: "right" }}>
                  {countEmployee}
                </div>
              </div>
              <div style={{ clear: "both" }}></div>
              <div style={{}}>Nhân viên</div>
            </div>
          </Card>
        </Col>
        <Col span={1}> </Col>
        <Col span={5}>
          <Card
            style={{
              backgroundColor: "red",
              color: "#fff",
              fontWeight: 700,
              fontSize: 20,
            }}
          >
            <div style={{ float: "right" }}>
              <div style={{}}>
                <div style={{ fontSize: 30, float: "right" }}>
                  {countShipment}
                </div>
              </div>
              <div style={{ clear: "both" }}></div>
              <div style={{}}>Lô hàng</div>
            </div>
          </Card>
        </Col>
        <Col span={1}> </Col>
        <Col span={5}>
          <Card
            style={{
              backgroundColor: "#008038ad",
              color: "#fff",
              fontWeight: 700,
              fontSize: 20,
            }}
          >
            <div style={{ float: "right" }}>
              <div style={{}}>
                <div style={{ fontSize: 30, float: "right" }}>
                  {countLongan}
                </div>
              </div>
              <div style={{ clear: "both" }}></div>
              <div style={{}}>Sản phẩm(kg)</div>
            </div>
          </Card>
        </Col>
        <Col span={1}> </Col>
        <Col span={5}>
          <Card
            style={{
              backgroundColor: "#660080ad",
              color: "#fff",
              fontWeight: 700,
              fontSize: 20,
            }}
          >
            <div style={{ float: "right" }}>
              <div style={{}}>
                <div style={{ fontSize: 30, float: "right" }}>123</div>
              </div>
              <div style={{ clear: "both" }}></div>
              <div style={{}}>Đã bán(kg) </div>
            </div>
          </Card>
        </Col>
      </Row>

      <Row>
        <Col span={10}>
          <Doughnut
            style={{}}
            data={{
              labels:
                longanDatas.length > 0
                  ? longanDatas.map((e) => e.categoryName)
                  : [],
              datasets: [
                {
                  label: "kg",
                  backgroundColor: [
                    "#3e95cd",
                    "#8e5ea2",
                    "#3cba9f",
                    "#e8c3b9",
                    "#c45850",
                    "#ff6384",
                    "#36a2eb",
                    "#cc65fe",
                    "#ffce56",
                    "#4bc0c0",
                  ],
                  data:
                    longanDatas.length > 0
                      ? longanDatas.map((e) => e.totalAmount)
                      : [],
                },
              ],
            }}
          />
          <div style={{ fontSize: 20, fontWeight: 600 }} span={10}>
            Biểu đồ tỉ lệ sản xuất giữa các sản phẩm{" "}
          </div>
        </Col>
        <Col span={4}></Col>
        <Col span={10}>
          <Line
            style={{ bottom: 44, position: "absolute" }}
            data={{
              labels:
                longanDatasYear.length > 0 ? longanDatasYear[0].listYear : [],
              datasets: longanDatasYear.map((e, index) => {
                return {
                  data: e.listTotalAmount,
                  label: e.categoryName,
                  borderColor: colors[index % colors.length],
                  fill: false,
                };
              }),
            }}
          />
          <div
            style={{
              fontSize: 20,
              fontWeight: 600,
              bottom: 0,
              position: "absolute",
            }}
            span={12}
          >
            Biểu đồ sản lượng sản xuất qua các năm{" "}
          </div>
        </Col>
      </Row>

      <Row></Row>
    </div>
  );
}

export default Home;
