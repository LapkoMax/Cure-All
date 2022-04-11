import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useParams } from "react-router-dom";
import { getAppointmentsForDoctor } from "../Api/AppointmentsData";
import {
  gettingAppointmentsAction,
  gotAppointmentsAction,
} from "../Store/ActionCreators/AppointmentActionCreators";
import { signOutUserAction } from "../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../Store/Reducers/RootReducer";
import { AppointmentList } from "./Appointments/AppointmentList";
import { Page } from "./General/Page";

export const AppointmentsListPage = () => {
  const { userId } = useParams();
  const dispatch = useDispatch();
  const location = useLocation();
  const appointments = useSelector(
    (state: AppState) => state.appointments.appointments,
  );
  const appointmentsLoading = useSelector(
    (state: AppState) => state.appointments.loading,
  );
  const user = useSelector((state: AppState) => state.identity.user);
  const userToken = useSelector((state: AppState) => state.identity.token);

  useEffect(() => {
    const doGetAppointments = async (userId?: string) => {
      dispatch(gettingAppointmentsAction());
      if (user?.type === "Doctor") {
        let result = await getAppointmentsForDoctor(userId, userToken);
        if (result.responseStatus === 401)
          dispatch(signOutUserAction(location.pathname));
        dispatch(gotAppointmentsAction(result.data));
      } else if (user?.type === "Patient") {
        // let result = await getAppointmentsForPatient(userId, userToken);
        // if (result.responseStatus === 401)
        //   dispatch(signOutUserAction(location.pathname));
        // dispatch(gotAppointmentsAction(result.data));
      }
    };
    doGetAppointments(userId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <div>
      {appointmentsLoading ? (
        <div>Загрузка...</div>
      ) : (
        <Page>
          <AppointmentList data={appointments} />
        </Page>
      )}
    </div>
  );
};
