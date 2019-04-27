export function processTransactions(transactionsList) {
    return async function (dispatch) {
        try {
            dispatch(doNothing(transactionsList));
        } catch (e) {
            console.log(e);
        }
    };
}

export function doNothing(transactionsList) {
    return { type: "typeString", actionObjKey: {} };
}