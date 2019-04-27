// https://stackoverflow.com/questions/14446511/most-efficient-method-to-groupby-on-a-array-of-objects
export function groupObjBy(xs, key) {
    return xs.reduce(function (rv, x) {
        (rv[x[key]] = rv[x[key]] || []).push(x);
        return rv;
    }, {});
};