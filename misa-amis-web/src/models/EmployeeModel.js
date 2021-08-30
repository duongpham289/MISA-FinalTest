class EmployeeModel {
    initData() {
        return  {
            EmployeeId: "",
            EmployeeCode: "",
            FullName: "",
            DateOfBirth: null,
            Gender: 1,
            IdentityNumber: "",
            IdentityDate: null,
            IdentityPlace: "",
            Email: "",
            MobilePhoneNumber: "",
            TelephoneNumber: "",
            PositionName: "",
            DepartmentId: "",
            DepartmentName: "",
            BankAccount: null,
            BankName: null,
            BankBranch: null,
        }
    }
}
export default new EmployeeModel();