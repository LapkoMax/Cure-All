import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useParams } from "react-router-dom";
import { getAppointment } from "../../../Api/AppointmentsData";
import {
  gettingAppointmentAction,
  gotAppointmentAction,
} from "../../../Store/ActionCreators/AppointmentActionCreators";
import { signOutUserAction } from "../../../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../../../Store/Reducers/RootReducer";
import { EditAppointment } from "../../Appointments/EditAppointment";
import { Page } from "../../General/Page";

export const EditAppointmentPage = () => {
  const { appointmentId } = useParams();
  const dispatch = useDispatch();
  const location = useLocation();
  const appointment = useSelector(
    (state: AppState) => state.appointments.appointment,
  );
  const appointmentLoading = useSelector(
    (state: AppState) => state.appointments.loading,
  );
  const userToken = useSelector((state: AppState) => state.identity.token);

  useEffect(() => {
    const doGetAppointment = async (appointmentId?: string) => {
      dispatch(gettingAppointmentAction());
      let result = await getAppointment(appointmentId, userToken);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      dispatch(gotAppointmentAction(result.data));
    };
    doGetAppointment(appointmentId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <Page title="Редактировать посещение:">
      {appointmentLoading ? (
        <div>Загрузка...</div>
      ) : (
        <EditAppointment appointment={appointment} />
      )}
    </Page>
  );
};
