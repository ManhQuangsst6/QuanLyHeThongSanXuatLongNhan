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
  Read,
} from "../../API/Notification/NotificationAPI";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min";

const NotificationComponent = () => {
  const navigate = useNavigate();
  const [page, setPage] = useState(1);
  const [countNotification, SetCountNotification] = useState(0);
  const [dataList, SetDataList] = useState([]);
  const [hasMore, setHasMore] = useState(true);

  const FormatDate = (data) => {
    const date = new Date(data);
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, "0");
    const day = date.getDate().toString().padStart(2, "0");
    const hours = date.getHours().toString().padStart(2, "0");
    const minutes = date.getMinutes().toString().padStart(2, "0");
    const formattedDate = `${year}-${month}-${day} ${hours}:${minutes}`;
    return formattedDate;
  };

  const fetchData = (page) => {
    setHasMore(false);
    GetListByPage(page, 8).then((res) => {
      const newItems = res.data.value.items.map((item) => ({
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
        isRead: item.isRead,
        link: item.link,
      }));

      SetDataList((prevItems) => [...prevItems, ...newItems]);
      if (newItems.length > 0) {
        setHasMore(true);
      }
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

    connection.on("activity", (data) => {
      GetCount().then((res) => {
        SetCountNotification(res.data.value);
      });
      setPage(1);
      SetDataList([]);
      fetchData(1);
    });

    return () => {
      connection.stop().then(() => console.log("Connection stopped"));
    };
  }, []);

  useEffect(() => {
    fetchData(page);
  }, [page]);

  const handleScroll = (e) => {
    const bottom =
      e.target.scrollHeight - e.target.scrollTop <= e.target.clientHeight + 20;
    if (bottom && hasMore) {
      setPage((prevPage) => prevPage + 1);
    }
  };

  const handleNotificationClick = (data) => {
    Read(data.key).then(() => {
      SetDataList((prevItems) =>
        prevItems.map((item) =>
          item.key === data.key ? { ...item, isRead: true } : item
        )
      );
      GetCount().then((res) => {
        SetCountNotification(res.data.value);
      });
      navigate(`/${data.link}`);
    });
  };

  return (
    <Dropdown>
      <Dropdown.Toggle
        as="div"
        variant="success"
        id="dropdown-basic"
        style={{ cursor: "pointer", display: "flex", alignItems: "center" }}
      >
        <Badge count={countNotification}>
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
          next={() => setPage((prevPage) => prevPage + 1)}
          hasMore={hasMore}
          loader={<p>Loading...</p>}
        >
          {dataList.map((season) => (
            <Dropdown.Item
              key={season.key}
              style={{
                lineHeight: "30px",
                backgroundColor: season.isRead === 0 ? "#f9f9f9" : "#ffffff",
              }}
              onClick={() => handleNotificationClick(season)}
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
