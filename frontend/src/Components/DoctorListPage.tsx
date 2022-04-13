import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { getDoctors } from "../Api/DoctorsData";
import {
  gettingDoctorsAction,
  gotDoctorsAction,
} from "../Store/ActionCreators/DoctorActionCreators";
import { signOutUserAction } from "../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../Store/Reducers/RootReducer";
import { DoctorList } from "./Doctors/DoctorList";
import { Page } from "./General/Page";

export const DoctorListPage = () => {
  const dispatch = useDispatch();
  const location = useLocation();
  const doctors = useSelector((state: AppState) => state.doctors.doctors);
  const doctorsLoading = useSelector(
    (state: AppState) => state.doctors.loading,
  );
  const userToken = useSelector((state: AppState) => state.identity.token);

  useEffect(() => {
    const doGetDoctors = async () => {
      dispatch(gettingDoctorsAction());
      var result = await getDoctors(userToken);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      dispatch(gotDoctorsAction(result.data));
    };
    doGetDoctors();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <Page>
      {doctorsLoading ? <div>Загрузка...</div> : <DoctorList data={doctors} />}
    </Page>
  );
};
