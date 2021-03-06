import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useParams } from "react-router-dom";
import { getDoctor } from "../../../Api/DoctorsData";
import {
  gettingDoctorAction,
  gotDoctorAction,
} from "../../../Store/ActionCreators/DoctorActionCreators";
import { signOutUserAction } from "../../../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../../../Store/Reducers/RootReducer";
import { AddAppointment } from "../../Appointments/AddAppointment";
import { Page } from "../../General/Page";

export const CreateAppointmentPage = () => {
  const { doctorId } = useParams();
  const dispatch = useDispatch();
  const location = useLocation();
  const doctor = useSelector((state: AppState) => state.doctors.doctor);
  const doctorLoading = useSelector((state: AppState) => state.doctors.loading);
  const userToken = useSelector((state: AppState) => state.identity.token);

  useEffect(() => {
    const doGetDoctor = async (doctorId?: string) => {
      dispatch(gettingDoctorAction());
      let result = await getDoctor(userToken, doctorId);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      dispatch(gotDoctorAction(result.data));
    };
    doGetDoctor(doctorId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <Page title="Запись на приём">
      {doctorLoading ? (
        <div>Загрузка...</div>
      ) : (
        <AddAppointment doctor={doctor}></AddAppointment>
      )}
    </Page>
  );
};
