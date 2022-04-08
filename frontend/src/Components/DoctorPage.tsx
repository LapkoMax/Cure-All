/** @jsxImportSource @emotion/react */
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import { getDoctor } from "../Api/DoctorsData";
import { deleteUser } from "../Api/IdentityData";
import {
  gettingDoctorAction,
  gotDoctorAction,
} from "../Store/ActionCreators/DoctorActionCreators";
import { signOutUserAction } from "../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../Store/Reducers/RootReducer";
import {
  DangerButton,
  FormButtonContainer,
  PrimaryButton,
} from "../Styles/Common/Buttons";
import {
  doctorContainer,
  doctorTitle,
} from "../Styles/Doctors/DoctorPageStyles";
import {
  doctorAdditionalInf,
  doctorExp,
  doctorSpecialzation,
} from "../Styles/Doctors/DoctorStyles";
import { Page } from "./General/Page";

type Props = {
  userId?: string;
};

export const DoctorPage = ({ userId }: Props) => {
  const dispatch = useDispatch();
  const location = useLocation();
  const navigate = useNavigate();
  const doctor = useSelector((state: AppState) => state.doctors.doctor);
  const doctorLoading = useSelector((state: AppState) => state.doctors.loading);
  const user = useSelector((state: AppState) => state.identity.user);
  const userToken = useSelector((state: AppState) => state.identity.token);

  useEffect(() => {
    const doGetDoctor = async (userId?: string) => {
      dispatch(gettingDoctorAction());
      let result = await getDoctor(userToken, userId);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      dispatch(gotDoctorAction(result.data));
    };
    doGetDoctor(userId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const editClickHandler = () => {
    navigate("/doctors/" + doctor?.id + "/edit");
  };

  const deleteClickHandler = async () => {
    let result = await deleteUser(
      user?.userName === undefined ? "" : user?.userName,
      userToken === undefined ? "" : userToken,
    );
    if (result) dispatch(signOutUserAction("/"));
  };

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
            <div css={doctorAdditionalInf}>
              <div>Номер лицензии: {doctor?.licenseNo}</div>
              <div>Страна: {doctor?.country}</div>
              <div>Город: {doctor?.city}</div>
              <div>Работает по адресу: {doctor?.workAddress}</div>
            </div>
          </div>
          {user?.id !== undefined && location.pathname.includes(user?.id) && (
            <div className="d-flex justify-content-center">
              <FormButtonContainer className="col-9 d-flex justify-content-around">
                <PrimaryButton onClick={editClickHandler}>
                  Редактировать профиль
                </PrimaryButton>
                <DangerButton onClick={deleteClickHandler}>
                  Удалить пользователя
                </DangerButton>
              </FormButtonContainer>
            </div>
          )}
        </Page>
      )}
    </div>
  );
};
