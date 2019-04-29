const ensureTwoDigits = function (num) {
    if (num < 9) {
        return '0' + num;
    }
    return "" + num;
};

export function dateToKeyString(dateObj) {
    return dateObj.getFullYear() + "-" + ensureTwoDigits(dateObj.getMonth() + 1) + "-" + ensureTwoDigits(dateObj.getDate()) + "T" + ensureTwoDigits(dateObj.getHours()) + ":" + ensureTwoDigits(dateObj.getMinutes()) + ":" + ensureTwoDigits(dateObj.getSeconds());
};

export function dateToDisplayDate(dateObj) {
    const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    return ensureTwoDigits(dateObj.getDate()) + "-" + monthNames[dateObj.getMonth()] + "-" + dateObj.getFullYear();
};

export function dateToApiStr(dateObj) {
    return dateObj.getFullYear() + "-" + ensureTwoDigits(dateObj.getMonth() + 1) + "-" + ensureTwoDigits(dateObj.getDate());
};