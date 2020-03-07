const ensureTwoDigits = (num: number): string => {
    if (num < 10) {
        return '0' + num;
    }
    return "" + num;
};

export const dateToKeyString = (dateObj: Date): string => {
    return dateObj.getFullYear() + "-" + ensureTwoDigits(dateObj.getMonth() + 1) + "-" + ensureTwoDigits(dateObj.getDate()) + "T" + ensureTwoDigits(dateObj.getHours()) + ":" + ensureTwoDigits(dateObj.getMinutes()) + ":" + ensureTwoDigits(dateObj.getSeconds());
};

export const dateToDisplayDate = (dateObj: Date): string => {
    //var dayNames = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
    const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    return ensureTwoDigits(dateObj.getDate()) + "-" + monthNames[dateObj.getMonth()] + "-" + dateObj.getFullYear();
};

export const dateToDayOfWeek = (dateObj: Date) => {
    const dayNames = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
    return dayNames[dateObj.getDay()];
};

export const dateToApiStr = (dateObj: Date): string => {
    return dateObj.getFullYear() + "-" + ensureTwoDigits(dateObj.getMonth() + 1) + "-" + ensureTwoDigits(dateObj.getDate());
};

export const dateTimeToApiStr = (dateObj: Date): string => {
    return `${dateToApiStr(dateObj)}T${ensureTwoDigits(dateObj.getHours())}:${ensureTwoDigits(dateObj.getMinutes())}:${ensureTwoDigits(dateObj.getSeconds())}`;
};