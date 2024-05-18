const ConvertDate = (inputDate) => {
    const date = new Date(inputDate);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Tháng bắt đầu từ 0
    const day = String(date.getDate()).padStart(2, '0');

    return `${day}/${month}/${year}`;
}
export default ConvertDate;
