var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
export const getFictMeasData = (baseAddr) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const resp = yield fetch(`${baseAddr}/api/Employees`, {
            method: 'get'
        });
        const respJSON = yield resp.json();
        //console.log(respJSON);
        return respJSON;
    }
    catch (e) {
        console.error(e);
        return [];
        //return { success: false, message: `Could not retrieve employees data due to error ${JSON.stringify(e)}` };
    }
});
//# sourceMappingURL=employees.js.map