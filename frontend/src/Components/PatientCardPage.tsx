/** @jsxImportSource @emotion/react */
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link, useLocation, useParams } from "react-router-dom";
import { getAppointmentsForPatientCard } from "../Api/AppointmentsData";
import { getPatientCard } from "../Api/PatientCardsData";
import {
  gettingAppointmentsAction,
  gotAppointmentsAction,
} from "../Store/ActionCreators/AppointmentActionCreators";
import { signOutUserAction } from "../Store/ActionCreators/IdentityActionCreators";
import {
  gettingPatientCardAction,
  gotPatientCardAction,
} from "../Store/ActionCreators/PatientCardActionCreators";
import { AppState } from "../Store/Reducers/RootReducer";
import {
  patientCardAdditionalInf,
  patientCardContainer,
  patientCardTitle,
} from "../Styles/PatientCards/PatientCardPageStyles";
import { AppointmentList } from "./Appointments/AppointmentList";
import { Page } from "./General/Page";

export const PatientCardPage = () => {
  const { patientCardId } = useParams();
  const dispatch = useDispatch();
  const location = useLocation();
  const patientCard = useSelector(
    (state: AppState) => state.patientCards.patientCard,
  );
  const patientCardLoading = useSelector(
    (state: AppState) => state.patientCards.loading,
  );
  const appointments = useSelector(
    (state: AppState) => state.appointments.appointments,
  );
  const appointmentsLoading = useSelector(
    (state: AppState) => state.appointments.loading,
  );
  const userToken = useSelector((state: AppState) => state.identity.token);

  useEffect(() => {
    const doGetPatientCard = async (patientCardId?: string) => {
      dispatch(gettingPatientCardAction());
      let result = await getPatientCard(patientCardId, userToken);
      if (result.status === 401) dispatch(signOutUserAction(location.pathname));
      dispatch(gotPatientCardAction(result.data));
    };

    const doGetAppointments = async (patientCardId?: string) => {
      dispatch(gettingAppointmentsAction());
      let result = await getAppointmentsForPatientCard(
        patientCardId,
        userToken,
      );
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      dispatch(gotAppointmentsAction(result.data));
    };

    doGetPatientCard(patientCardId);
    doGetAppointments(patientCardId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <Page title="Карта пациента">
      {patientCardLoading ? (
        <div>Загрузка...</div>
      ) : (
        <div css={patientCardContainer} className="row">
          <Link
            to={`/profile/${patientCard?.patientUserId}`}
            css={patientCardTitle}
            className="col-12 row"
          >
            Пациент: {patientCard?.patientFirstName}{" "}
            {patientCard?.patientLastName}
          </Link>
          <div css={patientCardAdditionalInf} className="row">
            <div>Посещения пациента:</div>
            {appointmentsLoading ? (
              <div>Загрузка...</div>
            ) : (
              <AppointmentList data={appointments} />
            )}
          </div>
        </div>
      )}
    </Page>
  );
};
