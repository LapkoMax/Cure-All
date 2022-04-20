import moment from "moment";
import { Fragment, useEffect, useRef, useState } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import {
  DayOfWeek,
  DaysOfWeek,
  DoctorData,
  DoctorDayOffData,
  DoctorSceduleData,
  editDoctor,
  EditDoctorForm,
  Statuses,
} from "../../Api/DoctorsData";
import {
  getSpecializations,
  SpecializationData,
} from "../../Api/SpecializationsData";
import { signOutUserAction } from "../../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../../Store/Reducers/RootReducer";
import {
  DangerButton,
  FormButtonContainer,
  PrimaryButton,
  SecondaryButton,
} from "../../Styles/Common/Buttons";
import {
  EditFieldset,
  FieldCheckBox,
  FieldContainer,
  FieldError,
  FieldInput,
  FieldLabel,
  FieldOption,
  FieldSelect,
} from "../../Styles/Common/FieldStyles";

type Props = {
  doctor?: DoctorData | null;
};

export const EditDoctor = ({ doctor }: Props) => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const location = useLocation();
  const {
    register,
    formState: { errors, isSubmitting },
    handleSubmit,
    watch,
    setValue,
  } = useForm<EditDoctorForm>({ mode: "onBlur" });
  const newPassword = useRef({});
  newPassword.current = watch("newPassword", "");
  const userToken = useSelector((state: AppState) => state.identity.token);
  const [loginErrors, setLoginErrors] = useState<string[] | undefined>([]);
  const [specializations, setSpecializations] = useState<SpecializationData[]>(
    [],
  );
  const [changePassword, setChangePassword] = useState(false);
  const [sceduledDayList, setSceduledDayList] = useState<DayOfWeek[]>([]);
  const [dayOffs, setDayOffs] = useState<DoctorDayOffData[]>([]);
  const [newDayOff, setNewDayOff] = useState<DoctorDayOffData>({
    status: "",
    statusName: "",
  });

  useEffect(() => {
    const doGetSpecializations = async () => {
      let results = await getSpecializations();
      setSpecializations(results);
    };
    doGetSpecializations();

    if (doctor != null) {
      console.log(doctor);
      setValue("firstName", doctor.firstName);
      setValue("lastName", doctor.lastName);
      setValue("userName", doctor.userName);
      setValue("phoneNumber", doctor.phoneNumber);
      setValue("email", doctor.email);
      setValue("licenseNo", doctor.licenseNo);
      setValue("workStart", moment(doctor?.workStart).format("yyyy-MM-DD"));
      setValue("workAddress", doctor.workAddress);
      setValue("averageAppointmentTime", doctor.averageAppointmentTime);
      setValue("workDayStart", doctor.workDayStart);
      setValue("workDayEnd", doctor.workDayEnd);
      setValue("dinnerStart", doctor.dinnerStart);
      setValue("dinnerEnd", doctor.dinnerEnd);
      setValue("dateOfBurth", moment(doctor?.dateOfBurth).format("yyyy-MM-DD"));
      setValue("zipCode", doctor.zipCode);
      setValue("country", doctor.country);
      setValue("city", doctor.city);
      setDayOffs(
        doctor.doctorDayOffs.map((docDayOff) => {
          return {
            id: docDayOff.id,
            doctorId: docDayOff.doctorId,
            date: docDayOff.date?.toString().substring(0, 10),
            status: docDayOff.status,
            statusName: Statuses.find((st) => st.value === docDayOff.status)
              ?.name as string,
          };
        }),
      );
      var newSceduledDaysOfWeek: DayOfWeek[] = [];
      doctor.doctorsScedule.forEach((day) =>
        newSceduledDaysOfWeek.push(
          DaysOfWeek.find(
            (dayOfWeek) => dayOfWeek.value === day.dayOfWeek,
          ) as DayOfWeek,
        ),
      );
      setSceduledDayList(newSceduledDaysOfWeek);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (doctor != null)
      setValue(
        "specializationId",
        specializations.find((spec) => spec.name === doctor.specialization)
          ?.id ?? "",
      );
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [specializations]);

  const submitForm = async (data: EditDoctorForm) => {
    setLoginErrors([]);
    data.doctorsScedule = sceduledDayList.map((day) => {
      return {
        id: doctor?.doctorsScedule.find((sced) => sced.dayOfWeek === day.value)
          ?.id,
        doctorId: doctor?.doctorsScedule.find(
          (sced) => sced.dayOfWeek === day.value,
        )?.doctorId,
        dayOfWeek: day.value,
      } as DoctorSceduleData;
    });
    data.doctorDayOffs = dayOffs.map((day) => {
      return {
        id: day.id,
        doctorId: day.doctorId,
        date: day.date,
        status: day.status,
      } as DoctorDayOffData;
    });
    const result = await editDoctor(data, userToken);
    if (result.length === 0) {
      navigate("/profile/" + doctor?.userId);
    } else if (result[0] === "Unauthorized") {
      dispatch(signOutUserAction(location.pathname));
    } else setLoginErrors(result);
  };

  const onDayOfWeekSelect = (dayOfWeek: DayOfWeek) => {
    let newSceduledDayList = sceduledDayList;
    if (newSceduledDayList.includes(dayOfWeek)) {
      newSceduledDayList = [];
      sceduledDayList.forEach((day) => {
        if (day.value !== dayOfWeek.value)
          newSceduledDayList = newSceduledDayList.concat(day);
      });
    } else newSceduledDayList = newSceduledDayList.concat(dayOfWeek);
    setSceduledDayList(newSceduledDayList);
  };

  const onDayOffDateSelect = (e: any) => {
    if (e.target.value !== "")
      setNewDayOff({ ...newDayOff, date: e.target.value });
  };

  const onDayOffStatusSelect = (e: any) => {
    if (e.target.value !== "") {
      let values = e.target.value.split(" ");
      setNewDayOff({ ...newDayOff, status: values[0], statusName: values[1] });
    }
  };

  const addNewDayOff = () => {
    if (newDayOff.date !== "" && newDayOff.status !== "") {
      setDayOffs(dayOffs.concat(newDayOff));
      setNewDayOff({ ...newDayOff, date: "", status: "", statusName: "" });
    }
  };

  const removeDayOff = (index: number) => {
    let newDayOffs: DoctorDayOffData[] = [];
    dayOffs.forEach((day, dayIndex) => {
      if (dayIndex !== index) newDayOffs.push(day);
    });
    setDayOffs(newDayOffs);
  };

  return (
    <form
      onSubmit={handleSubmit(submitForm)}
      className="row d-flex justify-content-around"
    >
      <EditFieldset
        disabled={isSubmitting}
        className="row d-flex justify-content-around"
      >
        <FieldInput {...register("id")} value={doctor?.id} hidden={true} />
        <FieldInput
          {...register("userId")}
          value={doctor?.userId}
          hidden={true}
        />
        <FieldInput
          {...register("oldUserName")}
          value={doctor?.userName}
          hidden={true}
        />
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="firstName">Имя</FieldLabel>
          <FieldInput
            id="firstName"
            {...register("firstName", {
              required: "Имя обязательно!",
              minLength: {
                value: 2,
                message: "Имя должно состоять минимум из 2 символов!",
              },
              maxLength: {
                value: 50,
                message: "Имя должно состоять максимум из 50 символов!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.firstName?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="lastName">Фамилия</FieldLabel>
          <FieldInput
            id="lastName"
            {...register("lastName", {
              required: "Фамилия обязательна!",
              minLength: {
                value: 2,
                message: "Фамилия должна состоять минимум из 2 символов!",
              },
              maxLength: {
                value: 50,
                message: "Фамилия должна состоять максимум из 50 символов!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.lastName?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="dateOfBurth">Дата рождения</FieldLabel>
          <FieldInput
            id="dateOfBurth"
            {...register("dateOfBurth", {
              required: "Дата рождения обязательна!",
            })}
            type="date"
            lang="ru-Cyrl-BY"
          />
          <FieldError>{errors.dateOfBurth?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="workStart">Дата начала работы</FieldLabel>
          <FieldInput
            id="workStart"
            {...register("workStart", {
              required: "Дата начала работы обязательна!",
            })}
            type="date"
            lang="ru-Cyrl-BY"
          />
          <FieldError>{errors.workStart?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-12 d-flex justify-content-center">
          <FieldLabel htmlFor="sceduledDays">
            Выберите свои рабочие дни(все остальные дни будут считаться
            выходными):
          </FieldLabel>
          <div id="sceduledDays">
            {DaysOfWeek.map((day) => (
              <FieldCheckBox
                id={day.id.toString()}
                className="form-check col-12"
              >
                <input
                  className="form-check-input mt-3"
                  type="checkbox"
                  value={day.value}
                  onChange={() => {
                    onDayOfWeekSelect(day);
                  }}
                  checked={sceduledDayList.includes(day)}
                />
                <FieldLabel className="form-check-label col-lg-10 col-md-11 col-sm-11 col-form-label text-left mt-1">
                  {day.name}
                </FieldLabel>
              </FieldCheckBox>
            ))}
          </div>
        </FieldContainer>
        <FieldContainer className="row col-12 d-flex justify-content-center">
          <FieldLabel htmlFor="dayOffs">
            Можете добавить специфические даты(приздник, отпуск и т. д.):
          </FieldLabel>
          <div
            id="dayOffs"
            className="col-12 row d-flex justify-content-around"
          >
            <div className="col-12 row">
              {dayOffs.map((day, index) => (
                <Fragment>
                  <FieldLabel className="col-10 row mb-2">{`${index + 1}. ${
                    day.date
                  } - ${day.statusName}`}</FieldLabel>
                  <DangerButton
                    className="col-2 row mb-2 btn btn-primary"
                    type="button"
                    onClick={() => {
                      removeDayOff(index);
                    }}
                  >
                    Удалить
                  </DangerButton>
                </Fragment>
              ))}
            </div>
            <div className="col-4 row">
              <FieldInput
                value={newDayOff.date}
                type="date"
                lang="ru-Cyrl-BY"
                onChange={onDayOffDateSelect}
              />
            </div>
            <div className="col-4 row">
              <FieldSelect
                value={`${newDayOff.status} ${newDayOff.statusName}`}
                onChange={onDayOffStatusSelect}
              >
                <FieldOption key={0} disabled value={" "}>
                  Можете выбрать статус
                </FieldOption>
                {Statuses.map((status) => (
                  <FieldOption
                    key={status.id}
                    value={`${status.value} ${status.name}`}
                  >
                    {status.name}
                  </FieldOption>
                ))}
              </FieldSelect>
            </div>
            <PrimaryButton
              className="col-4 row btn btn-primary"
              onClick={addNewDayOff}
              type="button"
            >
              Подтвердить
            </PrimaryButton>
          </div>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="zipCode">Почтовый код</FieldLabel>
          <FieldInput
            id="zipCode"
            {...register("zipCode", {
              required: "Почтовый код обязателен!",
              pattern: {
                value: /[0-9]{5}/,
                message: "Код должен состоять из 5 цифр!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.zipCode?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="phoneNumber">Номер телефона</FieldLabel>
          <FieldInput
            id="phoneNumber"
            {...register("phoneNumber", {
              required: "Номер телефона обязателен!",
              pattern: {
                value:
                  /^[+]?[(]?[0-9]{3}[)]?[-\s.]?[0-9]{3}[-\s.]?[0-9]{4,6}$/im,
                message: "Неверный формат номера!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.phoneNumber?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="country">Страна</FieldLabel>
          <FieldInput
            id="country"
            {...register("country", {
              required: "Страна обязательна!",
              minLength: {
                value: 3,
                message: "Название страны не может быть меньше 3 символов!",
              },
              maxLength: {
                value: 50,
                message: "Название страны не может быть больше 50 символов!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.country?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="city">Город</FieldLabel>
          <FieldInput
            id="city"
            {...register("city", {
              required: "Город обязателен!",
              minLength: {
                value: 3,
                message: "Название города не может быть меньше 3 символов!",
              },
              maxLength: {
                value: 50,
                message: "Название города не может быть больше 50 символов!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.city?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-12 d-flex justify-content-center">
          <FieldLabel htmlFor="specializationId">Специализация</FieldLabel>
          <FieldSelect
            id="specializationId"
            {...register("specializationId", {
              required: "Специализация обязательна!",
            })}
          >
            <FieldOption key={0} value="" disabled defaultValue="">
              Пожалуйста выберите специализацию
            </FieldOption>
            {specializations.map((spec) => (
              <FieldOption
                key={spec.id}
                value={spec.id}
                title={spec.description}
              >
                {spec.name}
              </FieldOption>
            ))}
          </FieldSelect>
          <FieldError>{errors.specializationId?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center mt-5">
          <FieldLabel className="mt-4" htmlFor="licenseNo">
            Номер лицензии
          </FieldLabel>
          <FieldInput
            id="licenseNo"
            {...register("licenseNo", {
              required: "Номер лицензии обязателен!",
            })}
            type="text"
          />
          <FieldError>{errors.licenseNo?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="averageAppointmentTime">
            Примерная продолжительность вашего приёма(в минутах):
          </FieldLabel>
          <FieldInput
            id="averageAppointmentTime"
            {...register("averageAppointmentTime", {
              valueAsNumber: true,
              required: "Продолжительность приёма обязательна!",
              min: { value: 5, message: "5 минут - минимальное время" },
              max: { value: 45, message: "45 минут - минимальное время" },
            })}
            type="number"
          />
          <FieldError>{errors.averageAppointmentTime?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="workDayStart">
            Начало вашего рабочего дня:
          </FieldLabel>
          <FieldInput
            id="workDayStart"
            {...register("workDayStart")}
            type="time"
          />
          <FieldError>{errors.workDayStart?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="workDayEnd">
            Конец вашего рабочего дня:
          </FieldLabel>
          <FieldInput id="workDayEnd" {...register("workDayEnd")} type="time" />
          <FieldError>{errors.workDayEnd?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="dinnerStart">
            Начало вашего обеденного перерыва:
          </FieldLabel>
          <FieldInput
            id="dinnerStart"
            {...register("dinnerStart")}
            type="time"
          />
          <FieldError>{errors.dinnerStart?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="dinnerEnd">
            Конец вашего обеденного перерыва:
          </FieldLabel>
          <FieldInput id="dinnerEnd" {...register("dinnerEnd")} type="time" />
          <FieldError>{errors.dinnerEnd?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="licenseNo">Номер лицензии</FieldLabel>
          <FieldInput
            id="licenseNo"
            {...register("licenseNo", {
              required: "Номер лицензии обязателен!",
            })}
            type="text"
          />
          <FieldError>{errors.licenseNo?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="workAddress">Адрес работы</FieldLabel>
          <FieldInput
            id="workAddress"
            {...register("workAddress", {
              required: "Адрес работы обязателен!",
            })}
            type="text"
          />
          <FieldError>{errors.workAddress?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="userName">Имя пользователя</FieldLabel>
          <FieldInput
            id="userName"
            {...register("userName", {
              required: "Имя пользоветеля обязательно!",
              minLength: {
                value: 2,
                message:
                  "Имя пользователя должно состоять минимум из 2 символов!",
              },
              maxLength: {
                value: 20,
                message:
                  "Имя пользователя должно состоять максимум из 20 символов!",
              },
              pattern: {
                value: /^[a-z]{2,}\d*$/i,
                message:
                  "Имя пользователя должно состоять из латинских символов, допускается наличие чисел!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.userName?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="email">Эл. почта</FieldLabel>
          <FieldInput
            id="email"
            {...register("email", {
              required: "Почта обязательна!",
              pattern: {
                value:
                  /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
                message: "Неверный формат адреса почты!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.email?.message}</FieldError>
        </FieldContainer>
        <PrimaryButton
          onClick={() => {
            setChangePassword(!changePassword);
          }}
          className="col-4"
          hidden={changePassword}
        >
          Сменить пароль
        </PrimaryButton>
        {changePassword && (
          <Fragment>
            <FieldContainer className="row col-7 d-flex justify-content-center">
              <FieldLabel htmlFor="oldPassword">Предыдущий пароль</FieldLabel>
              <FieldInput
                id="oldPassword"
                {...register("oldPassword")}
                type="password"
              />
              <FieldError>{errors.oldPassword?.message}</FieldError>
            </FieldContainer>
            <FieldContainer className="row col-7 d-flex justify-content-center">
              <FieldLabel htmlFor="newPassword">Новый пароль</FieldLabel>
              <FieldInput
                id="newPassword"
                {...register("newPassword", {
                  required: "Пароль обязателен!",
                  minLength: {
                    value: 8,
                    message: "Пароль должен состоять минимум из 8 символов!",
                  },
                  validate: {
                    latinCharacters: (value) =>
                      !new RegExp("^[а-яА-ЯёЁ]").test(value) ||
                      "Пароль не должен содержать кириллицу",
                    number: (value) =>
                      /\d/.test(value) ||
                      "Пароль должен содержать минимум 1 цифру",
                    capitalLetter: (value) =>
                      new RegExp("[A-Z]").test(value) ||
                      "Пароль должен содержать минимум 1 заглавный символ",
                    specialCharacters: (value) =>
                      /[\W_]+/g.test(value) ||
                      "Пароль должен содержать минимум 1 спец. символ",
                  },
                })}
                type="password"
              />
              <FieldError>{errors.newPassword?.message}</FieldError>
            </FieldContainer>
            <FieldContainer className="row col-7 d-flex justify-content-center">
              <FieldLabel htmlFor="confirmPassword">
                Подтвердите пароль
              </FieldLabel>
              <FieldInput
                id="confirmPassword"
                {...register("confirmPassword", {
                  required: "Подтверждение пароля обязательно!",
                  validate: (value) =>
                    value === newPassword.current ||
                    "Пароль подтверждён неверно",
                })}
                type="password"
              />
              <FieldError>{errors.confirmPassword?.message}</FieldError>
            </FieldContainer>
          </Fragment>
        )}
        {loginErrors &&
          loginErrors.map((error) => (
            <FieldError key={error}>{error}</FieldError>
          ))}
        <FormButtonContainer className="row d-flex justify-content-around">
          <PrimaryButton
            type="submit"
            className="col-4 d-flex justify-content-center"
          >
            Сохранить данные
          </PrimaryButton>
          <SecondaryButton
            onClick={() => {
              navigate("/profile/" + doctor?.userId);
            }}
            className="col-4 d-flex justify-content-center"
          >
            Назад
          </SecondaryButton>
        </FormButtonContainer>
      </EditFieldset>
    </form>
  );
};
