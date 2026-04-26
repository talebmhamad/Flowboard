

export const getToken = () => sessionStorage.getItem("token");

export const logout = () => sessionStorage.removeItem("token");