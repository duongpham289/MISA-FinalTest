export default class FormatData {
    //#region Định dạng dữ liệu ngày tháng
    /**
     * Format dữ liệu ngày sinh sang ngày/tháng/năm
     * @param {*} dob dữ liệu ngày sinh
     * CreatedBy: PHDUONG (29/08/2021)
     */
    static formatDate(str, onModal) {
        let vm = this;
        if (str == null || str.length === 0) return null;
        var date = new Date(str);
        return onModal ?
            vm.dateNum(date.getFullYear()) + '-' + vm.dateNum(date.getMonth() + 1) + '-' + vm.dateNum(date.getDate()) :
            vm.dateNum(date.getDate()) + '/' + vm.dateNum(date.getMonth() + 1) + '/' + vm.dateNum(date.getFullYear());
    }

    /**
     * Hàm định dạng cụ thể ngày và tháng
     * @param {*} num 1 con số bất kỳ
     * @returns nếu số x có 1 chữ số thì trả về dưới dạng '0x', nếu không thì trả về đúng số đó
     * CreatedBy: PHDUONG (29/08/2021)
     */
    static dateNum = (num) => {
        return num < 10 ? '0' + num : num
    };
    //#endregion

    /**
     * Định dạng Giới tính
     * @param {*} gender Giới tính dạng int
     * @returns  Giới tính dạng string
     * CreatedBy: PHDUONG (29/08/2021)
     */
    static formatGender(gender){
        if (gender === null || gender.length === 0) return '';
        var genders = ["Nữ", "Nam", "Khác"]
        if (gender < 3) {
            return gender = genders[gender];
        }
        return gender = 'Không có dữ liệu';
    }
}