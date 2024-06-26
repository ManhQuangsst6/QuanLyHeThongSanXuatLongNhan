// src/components/NotificationComponent.js
import React, { useEffect, useState } from "react";
import { Badge, Avatar } from "antd";
import { BellOutlined, UserOutlined } from "@ant-design/icons";
import * as signalR from "@microsoft/signalr";
import Dropdown from "react-bootstrap/Dropdown";
import InfiniteScroll from "react-infinite-scroll-component";
import "./styleNotification.scss";
import { useNavigate } from "react-router-dom"; 
import {
  GetCount,
  GetListByPage,
} from "../../API/Notification/NotificationAPI";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min";
import ConvertDate from "../../Common/Convert/ConvertDate";

const NotificationComponent = () => {
  const navigate = useNavigate();
  const [rootData, SetRootData] = useState([]);
  const [isRender, SetisRender] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [page, setPage] = useState(1);
  const [notifications, setNotifications] = useState([]);
  const [countNotification, SetCountNotification] = useState(null);
  const [dataList, SetDataList] = useState([]);
  function FormatDate(data) {
    var date = new Date(data);
    var year = date.getFullYear();
    var month = (date.getMonth() + 1).toString().padStart(2, "0");
    var day = date.getDate().toString().padStart(2, "0");
    var hours = date.getHours().toString().padStart(2, "0");
    var minutes = date.getMinutes().toString().padStart(2, "0");
    var seconds = date.getSeconds().toString().padStart(2, "0");
    var formattedDate =
      year + "-" + month + "-" + day + " " + hours + ":" + minutes;
    return formattedDate;
  }
  const [hasMore, setHasMore] = useState(false);
  const fetchData1 = () => {
    if (!hasMore) {
      setHasMore(true);
      setIsLoading(true);
      setPage((prevPage) => prevPage + 1);
      GetListByPage(page, 8).then((res) => {
        var data = res.data.value.items.map((item, index) => {
          return {
            label: (
              <div style={{ width: 300 }}>
                <div>
                  <strong>{item.createdBy}</strong>
                  {item.content}
                </div>
                <div>{FormatDate(item.created)}</div>
              </div>
            ),
            key: item.id,
          };
        });

        data = data.filter((e) => !dataList.find((a) => a.key === e.key));
        SetDataList((prevItems) => [...prevItems, ...data]);
        SetRootData((prevItems) => [...prevItems, ...data.map((e) => e.key)]);
        console.log(page);
        console.log(dataList);
        setIsLoading(false);
        setHasMore(false);
      });
    }
  };
  const fetchData = () => {
    GetListByPage(page, 8).then((res) => {
      var data = res.data.value.items.map((item, index) => {
        return {
          label: (
            <div style={{ width: 400, display: "flex" }}>
              <Avatar
                style={{ marginRight: 6 }}
                src="https://d1hjkbq40fs2x4.cloudfront.net/2016-01-31/files/1045-2.jpg"
                size="large"
                icon={<UserOutlined />}
              />
              <div style={{ width: 250 }}>
                <div>
                  <strong>{item.createdBy} </strong>
                  <span>{item.content}</span>
                </div>
                <div>{FormatDate(item.created)}</div>
              </div>
            </div>
          ),
          key: item.id,
        };
      });

      SetDataList(data);
    });
  };
  useEffect(() => {
    GetCount().then((res) => {
      SetCountNotification(res.data.value);
    });

    const connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7158/notificationHub")
      .withAutomaticReconnect()
      .build();

    connection
      .start()
      .then(() => console.log("Connection started"))
      .catch((err) =>
        console.log("Error while establishing connection: ", err)
      );

    connection.on("activity", (data) => {});
    return () => {
      connection.stop().then(() => console.log("Connection stopped"));
    };
  }, []);
  useEffect(() => {
    if (isRender === false) {
      fetchData();
      SetisRender(true);
    }
  }, [isRender]);

  const handleScroll = (e) => {
    const bottom =
      e.target.scrollHeight - e.target.scrollTop <= e.target.clientHeight + 20;
    if (bottom) {
      fetchData1();
    }
  };
  const handleNotificationClick = () => {
    navigate(`/salary`); // Điều hướng đến trang thông báo chi tiết
  };
  return (
    <Dropdown>
      <Dropdown.Toggle
        as="div"
        variant="success"
        id="dropdown-basic"
        style={{ cursor: "pointer", display: "flex", alignItems: "center" }}
      >
        <Badge count={5}>
          <BellOutlined style={{ fontSize: 24 }} />
        </Badge>
      </Dropdown.Toggle>
      <Dropdown.Menu
        id="style-5"
        style={{ maxHeight: "400px", overflowY: "auto" }}
        onScroll={handleScroll}
      >
        <InfiniteScroll
          dataLength={dataList.length}
          hasMore={hasMore}
          loader={<p>Loading...</p>}
        >
          {dataList.map((season, index) => (
            <Dropdown.Item
              style={{
                lineHeight: "30px",
                backgroundColor: index % 2 === 0 ? "#f9f9f9" : "#ffffff",
              }}
              onClick={handleNotificationClick}
            >
              {season.label}
            </Dropdown.Item>
          ))}
        </InfiniteScroll>
      </Dropdown.Menu>
    </Dropdown>
  );
};

export default NotificationComponent;
