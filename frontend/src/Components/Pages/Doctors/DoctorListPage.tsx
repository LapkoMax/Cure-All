/** @jsxImportSource @emotion/react */
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import {
  DoctorFields,
  DoctorParameters,
  getDoctors,
} from "../../../Api/DoctorsData";
import {
  getSpecializations,
  SpecializationData,
} from "../../../Api/SpecializationsData";
import {
  gettingDoctorsAction,
  gotDoctorsAction,
} from "../../../Store/ActionCreators/DoctorActionCreators";
import { signOutUserAction } from "../../../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../../../Store/Reducers/RootReducer";
import {
  FormButtonContainer,
  PrimaryButton,
} from "../../../Styles/Common/Buttons";
import {
  FieldCheckBox,
  FieldContainer,
  FieldInput,
  FieldLabel,
  FieldOption,
  FieldSelect,
} from "../../../Styles/Common/FieldStyles";
import {
  doctorListPageContainer,
  doctorParametersContainer,
} from "../../../Styles/Doctors/DoctorListPageStyles";
import { DoctorList } from "../../Doctors/DoctorList";
import { PageTitle } from "../../General/PageTitle";

export const DoctorListPage = () => {
  const dispatch = useDispatch();
  const location = useLocation();
  const {
    register,
    formState: { isSubmitting },
    handleSubmit,
  } = useForm<DoctorParameters>({ mode: "onBlur" });
  const doctors = useSelector((state: AppState) => state.doctors.doctors);
  const doctorsLoading = useSelector(
    (state: AppState) => state.doctors.loading,
  );
  const userToken = useSelector((state: AppState) => state.identity.token);
  const [parameters, setParameters] = useState<DoctorParameters>();
  const [specializations, setSpecializations] = useState<SpecializationData[]>(
    [],
  );
  const [byDescending, setByDescending] = useState(false);

  useEffect(() => {
    const doGetSpecializations = async () => {
      let results = await getSpecializations();
      setSpecializations(results);
    };
    doGetSpecializations();
  }, []);

  useEffect(() => {
    const doGetDoctors = async () => {
      dispatch(gettingDoctorsAction());
      var result = await getDoctors(userToken, parameters);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      dispatch(gotDoctorsAction(result.data));
    };
    doGetDoctors();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [parameters]);

  const submitForm = async (data: DoctorParameters) => {
    if (byDescending) data.orderBy = data.orderBy + " desc";
    setParameters(data);
  };

  return (
    <div className="col-12 row" css={doctorListPageContainer}>
      <div css={doctorParametersContainer} className="col-12 row">
        <PageTitle>Параметры фильтрации:</PageTitle>
        <form
          onSubmit={handleSubmit(submitForm)}
          className="row d-flex justify-content-around"
        >
          <FieldContainer className="col-4 row">
            <FieldLabel htmlFor="fullNameSearchTerm">Имя/фамилия:</FieldLabel>
            <FieldInput
              id="fullNameSearchTerm"
              {...register("fullNameSearchTerm")}
              type="text"
            />
          </FieldContainer>
          <FieldContainer className="col-4 row">
            <FieldLabel htmlFor="specialitySearchTerm">
              Специализация:
            </FieldLabel>
            <FieldSelect
              id="specialitySearchTerm"
              {...register("specialitySearchTerm")}
              defaultValue=""
            >
              <FieldOption key={0} value="" disabled>
                Можете выбрать специализацию
              </FieldOption>
              {specializations.map((spec) => (
                <FieldOption
                  key={spec.id}
                  value={spec.name}
                  title={spec.description}
                >
                  {spec.name}
                </FieldOption>
              ))}
            </FieldSelect>
          </FieldContainer>
          <FieldContainer className="col-4 row">
            <FieldLabel htmlFor="minExperienceYears">
              Минимально лет опыта:
            </FieldLabel>
            <FieldInput
              id="minExperienceYears"
              {...register("minExperienceYears")}
              type="number"
            />
          </FieldContainer>
          <FieldContainer className="col-3 row">
            <FieldLabel htmlFor="countrySearchTerm">Страна:</FieldLabel>
            <FieldInput
              id="countrySearchTerm"
              {...register("countrySearchTerm")}
              type="text"
            />
          </FieldContainer>
          <FieldContainer className="col-2 row">
            <FieldLabel htmlFor="citySearchTerm">Город:</FieldLabel>
            <FieldInput
              id="citySearchTerm"
              {...register("citySearchTerm")}
              type="text"
            />
          </FieldContainer>
          <FieldContainer className="col-3 row">
            <FieldLabel htmlFor="orderBy">Сортировать по:</FieldLabel>
            <FieldSelect id="orderBy" {...register("orderBy")}>
              {DoctorFields.map((field) => (
                <FieldOption key={field.id} value={field.name}>
                  {field.displayName}
                </FieldOption>
              ))}
            </FieldSelect>
          </FieldContainer>
          <div className="col-4 row pt-3">
            <FieldCheckBox id="isDescending" className="form-check">
              <input
                className="form-check-input mt-3"
                type="checkbox"
                onChange={() => {
                  setByDescending(!byDescending);
                }}
              />
              <FieldLabel className="form-check-label col-lg-10 col-md-11 col-sm-11 col-form-label text-left mt-1">
                По убыванию
              </FieldLabel>
            </FieldCheckBox>
          </div>
          <FormButtonContainer>
            <PrimaryButton type="submit" disabled={isSubmitting}>
              Подтвердить
            </PrimaryButton>
          </FormButtonContainer>
        </form>
      </div>
      <div>
        <PageTitle>Список докторов</PageTitle>
        {doctorsLoading ? (
          <div>Загрузка...</div>
        ) : (
          <DoctorList data={doctors} />
        )}
      </div>
    </div>
  );
};
