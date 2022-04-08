import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import { AppState } from "../Store/Reducers/RootReducer";
import { DoctorPage } from "./DoctorPage";

export const ProfilePage = () => {
  const { userId } = useParams();
  const user = useSelector((state: AppState) => state.identity.user);
  return <div>{user?.type === "Doctor" && <DoctorPage userId={userId} />}</div>;
};
