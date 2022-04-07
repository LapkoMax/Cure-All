/** @jsxImportSource @emotion/react */
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useParams } from "react-router-dom";
import { getDoctor } from "../Api/DoctorsData";
import {
  gettingDoctorAction,
  gotDoctorAction,
} from "../Store/ActionCreators/DoctorActionCreators";
import { signOutUserAction } from "../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../Store/Reducers/RootReducer";
import {
  doctorContainer,
  doctorTitle,
} from "../Styles/Doctors/DoctorPageStyles";
import { doctorExp, doctorSpecialzation } from "../Styles/Doctors/DoctorStyles";
import { Page } from "./General/Page";

export const DoctorPage = () => {
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
    <div>
      {doctorLoading ? (
        <div>Загрузка...</div>
      ) : (
        <Page>
          <div css={doctorContainer}>
            <div css={doctorTitle}>
              {doctor === null
                ? ""
                : `${doctor?.firstName} ${doctor?.lastName}`}
            </div>
            <div css={doctorSpecialzation}>
              Специальность: {doctor?.specialization}
            </div>
            <div css={doctorExp}>Опыт: {doctor?.yearsOfExperience} лет</div>
          </div>
        </Page>
      )}
    </div>
  );
};
