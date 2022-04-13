import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useParams } from "react-router-dom";
import { getUserById, UserData } from "../Api/IdentityData";
import { signOutUserAction } from "../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../Store/Reducers/RootReducer";
import { DoctorPage } from "./DoctorPage";
import { PatientPage } from "./PatientPage";

export const ProfilePage = () => {
  const { userId } = useParams();
  const dispatch = useDispatch();
  const location = useLocation();
  const [user, setUser] = useState<UserData | null>(null);
  const userToken = useSelector((state: AppState) => state.identity.token);

  useEffect(() => {
    const doGetUser = async (userId?: string) => {
      let result = await getUserById(userId, userToken);
      if (result.status === 401) dispatch(signOutUserAction(location.pathname));
      else if (result.status !== 404) setUser(result.data);
    };
    doGetUser(userId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [userId]);

  return (
    <div>
      {user?.type === "Doctor" && <DoctorPage user={user} />}
      {user?.type === "Patient" && <PatientPage user={user} />}
    </div>
  );
};
