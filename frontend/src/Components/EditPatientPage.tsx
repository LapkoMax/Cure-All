import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useParams } from "react-router-dom";
import { getPatient } from "../Api/PatientsData";
import { signOutUserAction } from "../Store/ActionCreators/IdentityActionCreators";
import {
  gettingPatientAction,
  gotPatientAction,
} from "../Store/ActionCreators/PatientActionCreators";
import { AppState } from "../Store/Reducers/RootReducer";
import { Page } from "./General/Page";
import { EditPatien } from "./Patients/EditPatient";

export const EditPatientPage = () => {
  const { patientId } = useParams();
  const dispatch = useDispatch();
  const location = useLocation();
  const patient = useSelector((state: AppState) => state.patients.patient);
  const patientLoading = useSelector(
    (state: AppState) => state.patients.loading,
  );
  const userToken = useSelector((state: AppState) => state.identity.token);

  useEffect(() => {
    const doGetPatient = async (patientId?: string) => {
      dispatch(gettingPatientAction());
      let result = await getPatient(userToken, patientId);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      dispatch(gotPatientAction(result.data));
    };
    doGetPatient(patientId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <Page title="Редактировать профиль:">
      {patientLoading ? (
        <div>Загрузка...</div>
      ) : (
        <EditPatien patient={patient} />
      )}
    </Page>
  );
};
