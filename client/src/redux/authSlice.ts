import { createSlice } from "@reduxjs/toolkit";

const authSlice = createSlice({
  name: "auth",
  initialState: {
    token: localStorage.getItem("token") || null,
    userType: localStorage.getItem("userType") || null,
  },
  reducers: {
    setUser: (state, action) => {
      state.token = action.payload.token;
      state.userType = action.payload.userType;
    },
    logout: (state) => {
      state.token = null;
      state.userType = null;
      localStorage.removeItem("token");
      localStorage.removeItem("userType");
    },
  },
});

export const { setUser, logout } = authSlice.actions;
export default authSlice.reducer;
