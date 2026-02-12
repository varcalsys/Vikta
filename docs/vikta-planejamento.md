# Vikta — Plataforma de Comunicação Escola-Família

> *Substituindo a agenda de papel, o grupo de WhatsApp e o app genérico por uma única plataforma digital, segura e organizada.*

---

## Índice

1. [Visão Geral](#1-visão-geral)
2. [O Problema Hoje](#2-o-problema-hoje)
3. [A Solução Vikta](#3-a-solução-vikta)
4. [Personas](#4-personas)
5. [Funcionalidades por Módulo](#5-funcionalidades-por-módulo)
6. [Arquitetura Técnica](#6-arquitetura-técnica)
7. [Domain Model (DDD)](#7-domain-model-ddd)
8. [Multi-Tenancy Strategy](#8-multi-tenancy-strategy)
9. [Banco de Dados](#9-banco-de-dados)
10. [Stack Tecnológica](#10-stack-tecnológica)
11. [Segurança e LGPD](#11-segurança-e-lgpd)
12. [Observabilidade](#12-observabilidade)
13. [MVP — Escopo e Priorização](#13-mvp--escopo-e-priorização)
14. [Roadmap](#14-roadmap)
15. [Modelo de Negócio](#15-modelo-de-negócio)
16. [Estrutura do Projeto (.NET)](#16-estrutura-do-projeto-net)
17. [Infraestrutura e DevOps](#17-infraestrutura-e-devops)
18. [Próximos Passos](#18-próximos-passos)

> **v1.2 — stack definitiva:** .NET 10 + Minimal API · Angular 19 (web) · Flutter 3.x (mobile)

---

## 1. Visão Geral

**Vikta** é uma plataforma SaaS multi-tenant B2B para escolas de educação infantil e fundamental, que centraliza toda a comunicação entre escola, professores e família em um único canal digital.

| | |
|---|---|
| **Nome** | Vikta |
| **Segmento** | EdTech / Comunicação Escolar |
| **Modelo** | SaaS B2B multi-tenant |
| **Público-alvo** | Escolas privadas de Ed. Infantil e Fundamental |
| **Plataformas** | Web (admin/professor) + App Mobile (pais) |
| **Mercado** | Brasil (LGPD compliant) |

---

## 2. O Problema Hoje

A comunicação escola-família nas escolas de educação infantil é **fragmentada em múltiplos canais desconectados**:

### 📓 Agenda de Papel (principal canal hoje)

A agenda física que vai e volta na mochila da criança todo dia é o canal mais usado. Um exemplo real do que a professora preenche manualmente todo dia:

```
Data: Qui 12/02/26

ALIMENTAÇÃO
  Lanche da manhã  → BOM
  Almoço           → BOM
  Lanche da tarde  → BOM
  Jantar           → BOM
  13:00 hs. 200 ml

SONO
  Dormiu: 11:40hs    Acordou: 12:45hs

HIGIENE
  Trocas: 7x    Evacuações: 1
  Mamou: 17:00 hs 250 ml

DISPOSIÇÃO: Normal ✓

MEDICAÇÃO: —

MAMÃE TRAZER (2ª feira):
  Pomada, Leite, Lenço umedecido

OBSERVAÇÕES:
  "Boa tarde! Hoje o pequeno se divertiu no parque
   e na palhinha com seus amigos.
   Por favor enviar 2ª feira leite, pomada e lenço
   umedecido. Obrigada!"
```

**Problemas da agenda física:**
- Pode ser perdida, esquecida ou molhada na mochila
- Professor gasta 5–10 min por aluno preenchendo à mão
- Pais precisam lembrar de abrir a mochila e verificar
- Não há confirmação de leitura
- Sem histórico organizado (folhas se acumulam)
- Impossível pesquisar informações passadas

### 📱 Grupo de WhatsApp (fotos e avisos da turma)
- Número pessoal da professora exposto
- Sem controle de horário (mensagens a qualquer hora)
- Fotos se perdem no histórico do grupo
- Pais de diferentes filhos em grupos diferentes, sem organização
- Sem confirmação de leitura para mensagens importantes
- Misto de urgente e trivial no mesmo canal

### 📲 App Genérico da Escola (comunicados administrativos)
- Avisos da diretoria e financeiro desconectados da realidade da sala
- Sem integração com a agenda da criança
- Interface genérica, sem identidade

### 🔍 Resultado
Os pais precisam checar 3 canais diferentes para ter a visão completa do dia do filho.

---

## 3. A Solução Vikta

**Uma plataforma que digitaliza a agenda, substitui o WhatsApp da turma e unifica os comunicados escolares.**

```
┌─────────────────────────────────────────────────────────┐
│                     HOJE                                 │
│                                                          │
│  📓 Agenda Física  +  📱 WhatsApp  +  📲 App Genérico   │
│       (5-10 min/aluno)   (grupos)    (comunicados)       │
└─────────────────────────────────────────────────────────┘
                          ↓  VIKTA
┌─────────────────────────────────────────────────────────┐
│                 PLATAFORMA VIKTA                          │
│                                                          │
│  📋 Agenda Digital  +  💬 Chat por Turma  +  📢 Mural   │
│  (30s/aluno, mobile)   (organizado)        (escola)      │
│                                                          │
│  + 📸 Galeria    + 📅 Calendário    + 💰 Financeiro      │
│                                                          │
│  + 📊 Relatórios     + 🔔 Notificações Push              │
└─────────────────────────────────────────────────────────┘
```

### Proposta de Valor

**Para a escola:**
- Reduz trabalho manual das professoras
- Comunicação profissional (sem WhatsApp pessoal)
- Controle de leitura dos comunicados
- Histórico organizado e pesquisável
- Imagem moderna para os pais

**Para os pais:**
- Tudo sobre o filho em um único lugar
- Notificação push no momento em que a agenda é preenchida
- Fotos organizadas por data/evento (não se perdem)
- Histórico completo de saúde, alimentação e sono
- Menos ansiedade: "o app vai avisar se tiver algo importante"

---

## 4. Personas

### 👩‍🏫 Maria — Professora de Educação Infantil
- 32 anos, professora há 8 anos
- Turma de 15 crianças de 2–3 anos
- Preenche agenda de papel todo dia para cada aluno
- Usa WhatsApp pessoal para falar com os pais
- **Dor:** Perde muito tempo escrevendo, não quer expor número pessoal
- **Objetivo com o Vikta:** Preencher a agenda rapidamente pelo celular e mandar fotos sem sair do app

### 👨 Rafael — Pai da Criança
- 35 anos, trabalha em TI
- Filho de 2 anos na escola
- Precisa checar agenda + WhatsApp todo dia
- Muitas vezes esquece de abrir a mochila
- **Dor:** Ansiedade de não saber como o filho passou o dia
- **Objetivo com o Vikta:** Receber notificação em tempo real quando a professora preencher a agenda

### 🏫 Diretora Carla — Gestora da Escola
- 45 anos, diretora há 15 anos
- Responsável por comunicados gerais, financeiro e matrículas
- Usa sistema ERP separado que os pais não acessam
- **Dor:** Falta de profissionalismo no WhatsApp, sem rastreabilidade de leitura
- **Objetivo com o Vikta:** Enviar comunicados com confirmação de leitura e integrar o financeiro

---

## 5. Funcionalidades por Módulo

### 📋 Módulo 1: Agenda Digital (substitui agenda de papel)

**Professor preenche (mobile-first):**

| Campo | Tipo | Detalhe |
|---|---|---|
| Alimentação | Seleção | Ótimo / Bom / Pouco / Recusou (por refeição) |
| Quantidade leite/mamadeira | Texto/número | Horário + ml |
| Sono | Time picker | Horário dormiu + acordou |
| Higiene - Trocas | Contador | Número de trocas |
| Higiene - Evacuações | Contador | Sim/Não + quantidade |
| Higiene - Banho | Toggle + horário | |
| Mamou (amamentação) | Toggle + horário + ml | |
| Disposição | Seleção | Agitado / Normal / Quieto |
| Medicação | Tabela | Horário / Remédio / Dosagem / Temperatura |
| Itens a trazer | Lista livre | Ex: pomada, lenço umedecido |
| Observações | Texto livre | Mensagem da professora |
| Fotos do dia | Upload múltiplo | Máx. 10 fotos |

**Pais visualizam:**
- Feed em tempo real com notificação push
- Timeline histórica por data
- Exportação PDF mensal (lembrança/médico)

### 💬 Módulo 2: Comunicação

**Chat por turma:**
- Canal por turma (substitui grupo WhatsApp)
- Professora e coordenação podem enviar para todos
- Horário configurável (ex: 7h–19h, fora disso: aviso de horário)
- Confirmação de leitura por pai
- Mensagem individual (professor → pai específico)

**Mural da escola (Comunicados):**
- Direção publica avisos para toda escola ou turmas específicas
- Categorias: Aviso, Urgente, Evento, Financeiro
- Confirmação de leitura obrigatória para avisos críticos
- Assinatura digital do responsável (LGPD)

### 📸 Módulo 3: Galeria

- Fotos por turma/data/evento
- Privacidade: pais veem apenas fotos onde o filho está (futuro: face detection opt-in)
- Download em lote (zip)
- Limite de armazenamento por plano

### 📅 Módulo 4: Calendário Escolar

O calendário é o **hub de tempo** da escola: onde pais e alunos sabem o que vem pela frente, e onde a gestão controla datas importantes do ano letivo.

**Escola / Coordenação cria:**

| Campo | Tipo | Detalhe |
|---|---|---|
| Título | Texto | "Reunião de Pais e Mestres" |
| Tipo | Seleção | Reunião / Feriado / Passeio / Festa / Reposição / Recesso / Outro |
| Escopo | Seleção | Toda escola / Turma(s) específicas / Aluno individual |
| Data início | Date picker | |
| Data fim | Date picker | Para eventos multi-dia (passeios, semanas temáticas) |
| Horário | Time picker | Opcional (eventos sem horário fixo ficam "dia inteiro") |
| Local | Texto | "Auditório", "Parque Villa-Lobos", URL do Google Meet |
| Descrição | Rich text | Detalhes, o que levar, como se vestir, etc. |
| Confirmação de presença | Toggle | Pais precisam confirmar (ex: reunião, festa) |
| Prazo de confirmação | Date picker | Disponível quando confirmação ativada |
| Lembrete automático | Seleção | 1 dia antes / 3 dias antes / 1 semana antes |
| Arquivo em anexo | Upload | PDF de autorização, programação do evento |

**Pais visualizam:**
- Calendário mensal/semanal/lista no app
- Destaque visual por tipo de evento (cores por categoria)
- Badge de "eventos esta semana" na home do app
- Confirmação de presença com 1 clique
- Exportação para Google Calendar / Apple Calendar (iCal)
- Notificação push no momento da publicação + lembrete automático

**Funcionalidades de gestão:**
- Visão global do calendário (coordenação vê todos os eventos)
- Relatório de confirmações de presença por evento
- Conflito de datas: alerta ao criar evento sobreposto
- Calendário letivo anual: importação de feriados nacionais (API feriados.com.br)
- Recorrência: eventos semanais/mensais (ex: "Musicalização — toda 3ª feira")

**Tipos de evento e ações associadas:**

| Tipo | Cor | Confirmação | Autorização PDF |
|---|---|---|---|
| Reunião | 🔵 Azul | Obrigatória | Não |
| Feriado / Recesso | ⚫ Cinza | Não | Não |
| Passeio | 🟢 Verde | Obrigatória | Sim (assinatura digital) |
| Festa / Apresentação | 🟡 Amarelo | Opcional | Não |
| Reposição de aula | 🟠 Laranja | Não | Não |
| Aviso geral | 🔴 Vermelho | Opcional | Não |

---

### 🍽️ Módulo 7: Cardápio Escolar

O cardápio resolve uma pergunta que **todo pai faz todo dia:** *"O que meu filho comeu hoje?"* — mas vai além: permite que os pais **planejem em casa** (compras, janta complementar, alergias) e a escola demonstre **transparência nutricional**.

**Escola / Nutricionista cadastra:**

| Campo | Tipo | Detalhe |
|---|---|---|
| Data | Date picker | Dia específico ou range de datas (semana, mês) |
| Refeição | Seleção | Lanche manhã / Almoço / Lanche tarde / Jantar |
| Prato principal | Texto | "Frango grelhado com arroz e feijão" |
| Acompanhamentos | Lista | "Cenoura refogada, couve manteiga" |
| Sobremesa | Texto | "Fruta da época (banana)" |
| Suco / Bebida | Texto | "Suco de maracujá natural" |
| Informação nutricional | Opcional | Calorias, proteínas, carboidratos (para planos Pro) |
| Alergênicos presentes | Multi-select | Glúten / Lactose / Ovo / Amendoim / Soja / Frutos do mar / Nozes |
| Opção vegetariana | Toggle | Se houver substituição disponível |
| Observação | Texto livre | "Cardápio sujeito a alterações conforme sazonalidade" |

**Pais visualizam:**
- Cardápio da semana na home do app (destaque)
- Notificação push toda sexta com cardápio da próxima semana
- Alerta personalizado: "⚠️ Cardápio de quinta contém **lactose**" (baseado no perfil do filho)
- Histórico de cardápios (útil para consultas médicas/nutricionistas)
- Botão "Meu filho tem restrição" — vincula alergia ao perfil do aluno

**Escola gerencia:**
- Cadastro semanal ou mensal (bulk create por semana)
- Cópia do cardápio da semana passada (template reutilizável)
- Publicação programada: cadastra semana, publica sexta às 18h automaticamente
- Controle de alterações: se cardápio muda no dia, push "Cardápio atualizado"

**Integrações internas:**
- **Agenda Digital (Módulo 1):** campo "Alimentação" da agenda pré-preenche o nome do prato do dia para facilitar o registro da professora
- **Perfil do aluno:** alergias cadastradas no perfil geram alertas automáticos no cardápio

**Domain Model — Cardápio:**

```csharp
// Domain/Nutrition/Aggregates/MenuPlan/MenuPlan.cs
public sealed class MenuPlan : AggregateRoot<MenuPlanId>
{
    public SchoolYear SchoolYear { get; private set; }
    public DateOnly WeekStart { get; private set; }   // sempre segunda-feira
    public MenuPlanStatus Status { get; private set; } // Draft | Published | Archived

    private readonly List<DailyMenu> _days = [];
    public IReadOnlyCollection<DailyMenu> Days => _days.AsReadOnly();

    public static MenuPlan Create(SchoolYear schoolYear, DateOnly weekStart)
    {
        if (weekStart.DayOfWeek != DayOfWeek.Monday)
            throw new DomainException("Week must start on Monday");

        var plan = new MenuPlan
        {
            Id = MenuPlanId.New(),
            SchoolYear = schoolYear,
            WeekStart = weekStart,
            Status = MenuPlanStatus.Draft
        };

        plan.RaiseDomainEvent(new MenuPlanCreatedEvent(plan.Id, weekStart));
        return plan;
    }

    public void AddDayMenu(DateOnly date, IReadOnlyList<MealEntry> meals)
    {
        if (date < WeekStart || date > WeekStart.AddDays(6))
            throw new DomainException("Date must be within the plan week");

        if (_days.Any(d => d.Date == date))
            throw new DomainException($"Menu for {date} already exists");

        var day = DailyMenu.Create(date, meals);
        _days.Add(day);
    }

    public void Publish()
    {
        if (!_days.Any())
            throw new DomainException("Cannot publish empty menu plan");

        Status = MenuPlanStatus.Published;
        RaiseDomainEvent(new MenuPlanPublishedEvent(Id, WeekStart));
    }

    public void UpdateDayMenu(DateOnly date, MealType mealType, MealDish dish)
    {
        var day = _days.FirstOrDefault(d => d.Date == date)
            ?? throw new DomainException("Day not found in this plan");

        day.UpdateMeal(mealType, dish);

        if (Status == MenuPlanStatus.Published)
            RaiseDomainEvent(new MenuPlanUpdatedEvent(Id, date, mealType));
    }
}

// Domain/Nutrition/Entities/DailyMenu.cs
public sealed class DailyMenu : Entity<DailyMenuId>
{
    public DateOnly Date { get; private set; }

    private readonly List<MealEntry> _meals = [];
    public IReadOnlyCollection<MealEntry> Meals => _meals.AsReadOnly();

    public static DailyMenu Create(DateOnly date, IReadOnlyList<MealEntry> meals)
        => new() { Id = DailyMenuId.New(), Date = date, _meals = meals.ToList() };

    public void UpdateMeal(MealType type, MealDish dish)
    {
        var meal = _meals.FirstOrDefault(m => m.MealType == type)
            ?? throw new DomainException($"Meal {type} not found");

        meal.UpdateDish(dish);
    }
}

// Domain/Nutrition/ValueObjects/MealDish.cs
public sealed class MealDish : ValueObject
{
    public string MainDish { get; private set; }
    public IReadOnlyList<string> Sides { get; private set; }
    public string? Dessert { get; private set; }
    public string? Beverage { get; private set; }
    public IReadOnlyList<Allergen> Allergens { get; private set; }
    public bool HasVegetarianOption { get; private set; }
    public string? Observation { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return MainDish;
        foreach (var side in Sides) yield return side;
    }
}

// Domain/Nutrition/ValueObjects/Allergen.cs
public enum Allergen
{
    Gluten, Lactose, Egg, Peanut, Soy, Shellfish, TreeNuts, Fish, Sesame
}

// Domain/Nutrition/DomainEvents/
// MenuPlanCreatedEvent, MenuPlanPublishedEvent, MenuPlanUpdatedEvent
```

**Domain Model — Calendário:**

```csharp
// Domain/Communication/Aggregates/CalendarEvent/CalendarEvent.cs
public sealed class CalendarEvent : AggregateRoot<CalendarEventId>
{
    public string Title { get; private set; }
    public CalendarEventType Type { get; private set; }
    public EventScope Scope { get; private set; }        // School | Classroom | Student
    public Guid? ScopeEntityId { get; private set; }    // ClassroomId se Scope = Classroom
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }       // null = evento de 1 dia
    public TimeOnly? StartTime { get; private set; }
    public string? Location { get; private set; }
    public string? Description { get; private set; }
    public bool RequiresConfirmation { get; private set; }
    public DateOnly? ConfirmationDeadline { get; private set; }
    public ReminderConfig Reminder { get; private set; }
    public EventStatus Status { get; private set; }

    private readonly List<EventAttachment> _attachments = [];
    private readonly List<AttendanceConfirmation> _confirmations = [];

    public IReadOnlyCollection<EventAttachment> Attachments => _attachments.AsReadOnly();
    public IReadOnlyCollection<AttendanceConfirmation> Confirmations => _confirmations.AsReadOnly();

    public static CalendarEvent Create(
        string title,
        CalendarEventType type,
        EventScope scope,
        DateOnly startDate,
        CreatedBy createdBy)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Event title is required");

        var evt = new CalendarEvent
        {
            Id = CalendarEventId.New(),
            Title = title,
            Type = type,
            Scope = scope,
            StartDate = startDate,
            Status = EventStatus.Draft,
        };

        evt.RaiseDomainEvent(new CalendarEventCreatedEvent(evt.Id, startDate, scope));
        return evt;
    }

    public void Publish()
    {
        Status = EventStatus.Published;
        RaiseDomainEvent(new CalendarEventPublishedEvent(Id, StartDate, Scope, ScopeEntityId));
    }

    public void ConfirmAttendance(UserId parentId, bool attending, string? note = null)
    {
        if (!RequiresConfirmation)
            throw new DomainException("This event does not require confirmation");

        if (ConfirmationDeadline.HasValue && DateOnly.FromDateTime(DateTime.UtcNow) > ConfirmationDeadline)
            throw new DomainException("Confirmation deadline has passed");

        var existing = _confirmations.FirstOrDefault(c => c.ParentId == parentId);
        if (existing is not null)
        {
            existing.Update(attending, note);
        }
        else
        {
            _confirmations.Add(AttendanceConfirmation.Create(parentId, attending, note));
        }

        RaiseDomainEvent(new AttendanceConfirmedEvent(Id, parentId, attending));
    }
}

// Enums
public enum CalendarEventType
{
    Meeting, Holiday, FieldTrip, Party, MakeupClass, Recess, General
}

public enum EventScope { School, Classroom, Student }
public enum EventStatus { Draft, Published, Cancelled }
```

---

### 💰 Módulo 5: Financeiro (básico)

- Visualização de mensalidades e status (pago/vencido/pendente)
- Geração de boleto e PIX
- Histórico de pagamentos
- Notificação de vencimento

### 📊 Módulo 6: Relatórios e Gestão (administrativo)

- Frequência dos alunos
- Relatório nutricional (histórico de alimentação + cardápio da semana)
- Relatório de sono
- Turmas e alunos por professor
- Exportação para Excel/PDF

---

## 6. Arquitetura Técnica

### Abordagem: Modular Monolith → Microsserviços (evolutivo)

Começar com **Modular Monolith** para velocidade de desenvolvimento, mantendo boundaries claros que permitem extração futura de microsserviços.

```
┌─────────────────────────────────────────────────────────────────┐
│                      PRESENTATION LAYER                          │
├──────────────────────────────┬──────────────────────────────────┤
│   Mobile App                 │   Web Portal                      │
│   Flutter (iOS/Android)      │   Angular 19                      │
│   - Parent App               │   - School Admin Dashboard        │
│   - Teacher App              │   - Teacher Interface             │
└──────────────────────────────┴──────────────────────────────────┘
                              ↓ HTTPS / WSS
┌─────────────────────────────────────────────────────────────────┐
│                        API GATEWAY                               │
│   NGINX / YARP Reverse Proxy                                      │
│   - Rate Limiting (per tenant)                                   │
│   - Tenant Resolution (subdomain / JWT claim)                    │
│   - JWT Validation                                               │
│   - Request Logging / OpenTelemetry Trace Propagation            │
└─────────────────────────────────────────────────────────────────┘
                              ↓
┌─────────────────────────────────────────────────────────────────┐
│              MODULAR MONOLITH — Vikta.Api (.NET 10)              │
├──────────────┬──────────────┬──────────────┬────────────────────┤
│ Communication│   Academic   │  Financial   │   Identity         │
│   Module     │   Module     │   Module     │   Module           │
│              │              │              │                    │
│ ■ Timeline   │ ■ Student    │ ■ Invoice    │ ■ Users            │
│ ■ Message    │ ■ Classroom  │ ■ Payment    │ ■ Roles            │
│ ■ Gallery    │ ■ Attendance │ ■ PIX/Boleto │ ■ JWT/Refresh      │
│ ■ Mural      │ ■ Schedule   │ ■ Subscription│ ■ Permissions     │
│ ■ Calendar   │              │              │                    │
│ ■ Notification│             │              │                    │
├──────────────┴──────────────┴──────────────┴────────────────────┤
│                     Nutrition Module                             │
│   ■ MenuPlan (Cardápio)   ■ DailyMenu   ■ AllergenAlert         │
└─────────────────────────────────────────────────────────────────┘
                              ↓
┌─────────────────────────────────────────────────────────────────┐
│                      SHARED KERNEL                               │
│   - Multi-Tenancy Context (TenantId, SchemaName)                 │
│   - Domain Events Bus (MediatR)                                  │
│   - Result<T> Pattern                                            │
│   - Audit Log                                                    │
│   - OpenTelemetry Instrumentation                                │
└─────────────────────────────────────────────────────────────────┘
                              ↓
┌────────────┬─────────────┬──────────────┬────────────────────────┐
│ PostgreSQL │    Redis    │  RabbitMQ    │   Blob Storage          │
│ (Schema/  │   (Cache,   │  (Domain →   │   MinIO / S3            │
│  Tenant)  │   SignalR   │   Integration│   (fotos, PDFs,         │
│           │   Backplane)│   Events)    │    autorizações)        │
└────────────┴─────────────┴──────────────┴────────────────────────┘
```

### Fluxo de uma entrada na Agenda Digital

```
Professor (mobile)
    │
    │ POST /api/timeline/{studentId}/entries
    ▼
API Gateway
    │ Resolve tenant_id do JWT
    │ Rate limiting check
    ▼
Timeline Module
    │
    ├── Application: AddTimelineEntryCommandHandler
    │       ├── Valida (FluentValidation)
    │       ├── Fetch/Create Timeline aggregate
    │       ├── timeline.AddMealEntry(...)     ← Domain Logic
    │       ├── Upload fotos → S3
    │       └── SaveAsync → PostgreSQL (schema: tenant_escola_001)
    │
    ├── Domain Event: TimelineEntryAddedEvent
    │       └── Handler → publica no RabbitMQ
    │
    └── RabbitMQ Consumer: NotificationService
            └── Firebase Push Notification → pais
```

---

## 7. Domain Model (DDD)

### Bounded Contexts

```
┌─────────────────────────────────────────────────────────────┐
│                  COMMUNICATION CONTEXT                       │
│                                                              │
│  Timeline (Aggregate Root)                                   │
│    └── TimelineEntry (Entity)                                │
│          └── Photo (Value Object)                            │
│                                                              │
│  Message (Aggregate Root)                                    │
│    └── MessageReadReceipt (Entity)                           │
│                                                              │
│  Announcement (Aggregate Root)  [Mural]                      │
│    └── AnnouncementConfirmation (Entity)                     │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                    ACADEMIC CONTEXT                          │
│                                                              │
│  Student (Aggregate Root)                                    │
│    └── Enrollment (Entity)                                   │
│    └── ParentLink (Entity)                                   │
│                                                              │
│  Classroom (Aggregate Root)                                  │
│    └── TeacherAssignment (Entity)                            │
│                                                              │
│  AttendanceRecord (Aggregate Root)                           │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                   FINANCIAL CONTEXT                          │
│                                                              │
│  Invoice (Aggregate Root)                                    │
│    └── Payment (Entity)                                      │
│                                                              │
│  Subscription (Aggregate Root)  [plano da escola no SaaS]    │
│    └── SubscriptionPlan (Value Object)                       │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                  NUTRITION CONTEXT  🍽️                      │
│                                                              │
│  MenuPlan (Aggregate Root)     ← semana completa             │
│    └── DailyMenu (Entity)      ← dia específico              │
│          └── MealEntry (Entity)                              │
│                └── MealDish (Value Object) ← prato + acomp. │
│                └── Allergen[] (Value Object)                 │
│                                                              │
│  StudentAllergyProfile (Entity) ← alergias do aluno          │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                  CALENDAR CONTEXT  📅                        │
│                                                              │
│  CalendarEvent (Aggregate Root)                              │
│    └── EventAttachment (Entity)   ← PDFs de autorização      │
│    └── AttendanceConfirmation (Entity) ← confirmação pais    │
│    └── ReminderConfig (Value Object)                         │
│    └── EventScope (Value Object)  ← escola/turma/aluno       │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                   IDENTITY CONTEXT                           │
│                                                              │
│  User (Aggregate Root)                                       │
│    └── UserRole (Entity)                                     │
│    └── UserType: Parent | Teacher | Coordinator | Admin      │
└─────────────────────────────────────────────────────────────┘
```

### Timeline — Aggregate Root (detalhado)

```csharp
// Domain/Communication/Aggregates/Timeline/Timeline.cs
public sealed class Timeline : AggregateRoot<TimelineId>
{
    public StudentId StudentId { get; private set; }
    public ClassroomId ClassroomId { get; private set; }
    public Date Date { get; private set; }
    public TimelineStatus Status { get; private set; }

    private readonly List<TimelineEntry> _entries = [];
    public IReadOnlyCollection<TimelineEntry> Entries => _entries.AsReadOnly();

    private Timeline() { }

    public static Timeline Create(StudentId studentId, ClassroomId classroomId, Date date)
    {
        var timeline = new Timeline
        {
            Id = TimelineId.New(),
            StudentId = studentId,
            ClassroomId = classroomId,
            Date = date,
            Status = TimelineStatus.Draft
        };
        timeline.RaiseDomainEvent(new TimelineCreatedEvent(timeline.Id, studentId, date));
        return timeline;
    }

    public void AddMealEntry(MealType type, MealQuality quality, TeacherId teacherId)
    {
        GuardAgainstDuplicateMealType(type);
        var entry = TimelineEntry.CreateMeal(type, quality, teacherId);
        _entries.Add(entry);
        RaiseDomainEvent(new TimelineEntryAddedEvent(Id, StudentId, entry));
    }

    public void AddNapEntry(TimeOnly sleepTime, TimeOnly wakeTime, TeacherId teacherId)
    {
        if (wakeTime <= sleepTime)
            throw new DomainException("Wake time must be after sleep time");

        var duration = wakeTime - sleepTime;
        if (duration.TotalHours > 5)
            throw new DomainException("Nap duration exceeds allowed maximum");

        var entry = TimelineEntry.CreateNap(sleepTime, wakeTime, teacherId);
        _entries.Add(entry);
        RaiseDomainEvent(new TimelineEntryAddedEvent(Id, StudentId, entry));
    }

    public void AddActivityEntry(
        string description,
        IReadOnlyList<Photo> photos,
        TeacherId teacherId)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Activity description is required");
        if (photos.Count > 10)
            throw new DomainException("Maximum 10 photos per activity entry");

        var entry = TimelineEntry.CreateActivity(description, photos, teacherId);
        _entries.Add(entry);
        RaiseDomainEvent(new TimelineEntryAddedEvent(Id, StudentId, entry));
    }

    public void Publish(TeacherId teacherId)
    {
        if (Status == TimelineStatus.Published)
            throw new DomainException("Timeline already published");

        Status = TimelineStatus.Published;
        RaiseDomainEvent(new TimelinePublishedEvent(Id, StudentId, teacherId, Date));
    }

    private void GuardAgainstDuplicateMealType(MealType type)
    {
        if (_entries.OfType<MealTimelineEntry>().Any(e => e.MealType == type))
            throw new DomainException($"Meal entry for {type} already exists today");
    }
}
```

### Value Objects

```csharp
// Photo (Value Object)
public sealed class Photo : ValueObject
{
    public string StorageKey { get; private set; }   // S3 key
    public string ThumbnailKey { get; private set; }
    public long SizeInBytes { get; private set; }
    public DateTime UploadedAt { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StorageKey;
    }
}

// MealQuality (Value Object / Enum)
public enum MealQuality { Outstanding, Good, Little, Refused }

// TimelineEntryType
public enum TimelineEntryType
{
    Meal, Nap, Hygiene, Medication, Activity, Observation, ItemRequest
}
```

---

## 8. Multi-Tenancy Strategy

### Schema per Tenant (PostgreSQL)

**Justificativa:**
- Isolamento de dados forte → LGPD compliance por padrão
- Backup/restore independente por escola
- Migrations independentes por tenant
- Performance superior ao row-level (sem filtros globais)
- Suporta até ~1.000 schemas por database

```
Database: vikta_production
├── schema: tenant_escola_001  (Escola Montessori ABC)
├── schema: tenant_escola_002  (Colégio São Paulo)
├── schema: tenant_escola_003  (Escola Arco-Íris)
└── schema: public             (tabelas de controle global)

public:
  - tenants              (id, slug, name, plan, schema_name, created_at)
  - tenant_subscriptions (billing, plan, status)
```

### Tenant Resolution

```csharp
// TenantMiddleware resolve por subdomain OU JWT claim
// escola001.vikta.com.br → tenant_id: escola001
// Header X-Tenant-ID como fallback (mobile)

public sealed class TenantMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ITenantResolver resolver)
    {
        string tenantSlug;

        // 1. Tenta resolver por subdomain
        var host = context.Request.Host.Host;
        if (host.Contains('.'))
        {
            tenantSlug = host.Split('.')[0];
        }
        else
        {
            // 2. Fallback: JWT claim (mobile app)
            tenantSlug = context.User.FindFirstValue("tenant_id")
                ?? throw new UnauthorizedAccessException("Tenant not identified");
        }

        var tenant = await resolver.ResolveAsync(tenantSlug, context.RequestAborted);
        if (tenant is null) throw new TenantNotFoundException(tenantSlug);

        context.Items[TenantContextKeys.TenantId] = tenant.Id;
        context.Items[TenantContextKeys.SchemaName] = tenant.DatabaseSchema;

        await next(context);
    }
}
```

---

## 9. Banco de Dados

### Principais Tabelas (por schema de tenant)

```sql
-- ============================================================
-- ACADEMIC
-- ============================================================
CREATE TABLE students (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    full_name       VARCHAR(200) NOT NULL,
    date_of_birth   DATE NOT NULL,
    classroom_id    UUID NOT NULL REFERENCES classrooms(id),
    photo_url       TEXT,
    active          BOOLEAN DEFAULT TRUE,
    created_at      TIMESTAMPTZ DEFAULT NOW(),
    updated_at      TIMESTAMPTZ DEFAULT NOW()
);

CREATE TABLE classrooms (
    id          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name        VARCHAR(100) NOT NULL,   -- "Turma dos Pintinhos"
    grade       VARCHAR(50),             -- "Berçário 2", "Maternal I"
    school_year INT NOT NULL,
    created_at  TIMESTAMPTZ DEFAULT NOW()
);

CREATE TABLE student_parents (
    student_id  UUID NOT NULL REFERENCES students(id),
    user_id     UUID NOT NULL,           -- ref ao identity context
    relationship VARCHAR(50) NOT NULL,   -- "Pai", "Mãe", "Avó", etc.
    is_primary  BOOLEAN DEFAULT FALSE,
    PRIMARY KEY (student_id, user_id)
);

-- ============================================================
-- COMMUNICATION — TIMELINE (Agenda Digital)
-- ============================================================
CREATE TABLE timelines (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    student_id      UUID NOT NULL REFERENCES students(id),
    classroom_id    UUID NOT NULL REFERENCES classrooms(id),
    date            DATE NOT NULL,
    status          VARCHAR(20) NOT NULL DEFAULT 'draft',  -- draft | published
    created_at      TIMESTAMPTZ DEFAULT NOW(),
    UNIQUE (student_id, date)
);

CREATE TABLE timeline_entries (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    timeline_id     UUID NOT NULL REFERENCES timelines(id) ON DELETE CASCADE,
    entry_type      VARCHAR(50) NOT NULL,   -- meal | nap | hygiene | medication | activity | observation | item_request
    payload         JSONB NOT NULL,         -- dados específicos do tipo
    teacher_id      UUID NOT NULL,
    recorded_at     TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Exemplo de payload por tipo:
-- meal:       {"meal_type": "lunch", "quality": "good", "quantity_ml": 200}
-- nap:        {"sleep_time": "11:40", "wake_time": "12:45"}
-- hygiene:    {"diaper_changes": 7, "evacuations": 1, "bath": false}
-- medication: {"medicine": "Dipirona", "dose": "2ml", "time": "13:00", "temperature": 37.5}
-- item_request: {"items": ["leite", "pomada", "lenço umedecido"], "needed_by": "2026-02-16"}

CREATE TABLE timeline_photos (
    id                  UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    timeline_entry_id   UUID NOT NULL REFERENCES timeline_entries(id) ON DELETE CASCADE,
    storage_key         TEXT NOT NULL,
    thumbnail_key       TEXT NOT NULL,
    size_bytes          BIGINT,
    sort_order          INT DEFAULT 0,
    uploaded_at         TIMESTAMPTZ DEFAULT NOW()
);

-- ============================================================
-- COMMUNICATION — MESSAGES & MURAL
-- ============================================================
CREATE TABLE messages (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    channel_type    VARCHAR(30) NOT NULL,  -- classroom | individual | announcement
    channel_id      UUID NOT NULL,         -- classroom_id ou user_id
    sender_id       UUID NOT NULL,
    content         TEXT NOT NULL,
    has_attachments BOOLEAN DEFAULT FALSE,
    sent_at         TIMESTAMPTZ DEFAULT NOW()
);

CREATE TABLE message_receipts (
    message_id  UUID NOT NULL REFERENCES messages(id) ON DELETE CASCADE,
    user_id     UUID NOT NULL,
    read_at     TIMESTAMPTZ,
    PRIMARY KEY (message_id, user_id)
);

-- ============================================================
-- FINANCIAL
-- ============================================================
CREATE TABLE invoices (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    student_id      UUID NOT NULL REFERENCES students(id),
    description     VARCHAR(200) NOT NULL,
    amount          NUMERIC(10,2) NOT NULL,
    due_date        DATE NOT NULL,
    status          VARCHAR(20) NOT NULL DEFAULT 'pending', -- pending | paid | overdue | cancelled
    boleto_url      TEXT,
    pix_code        TEXT,
    paid_at         TIMESTAMPTZ,
    created_at      TIMESTAMPTZ DEFAULT NOW()
);

-- ============================================================
-- NUTRITION — CARDÁPIO
-- ============================================================
CREATE TABLE menu_plans (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    school_year     INT NOT NULL,
    week_start      DATE NOT NULL,              -- sempre segunda-feira
    status          VARCHAR(20) NOT NULL DEFAULT 'draft',  -- draft | published | archived
    created_by      UUID NOT NULL,
    published_at    TIMESTAMPTZ,
    created_at      TIMESTAMPTZ DEFAULT NOW(),
    UNIQUE (school_year, week_start)
);

CREATE TABLE daily_menus (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    menu_plan_id    UUID NOT NULL REFERENCES menu_plans(id) ON DELETE CASCADE,
    date            DATE NOT NULL,
    UNIQUE (menu_plan_id, date)
);

CREATE TABLE meal_entries (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    daily_menu_id   UUID NOT NULL REFERENCES daily_menus(id) ON DELETE CASCADE,
    meal_type       VARCHAR(30) NOT NULL,       -- morning_snack | lunch | afternoon_snack | dinner
    main_dish       VARCHAR(300) NOT NULL,      -- "Frango grelhado com arroz e feijão"
    sides           TEXT[],                     -- ["Cenoura refogada", "Couve manteiga"]
    dessert         VARCHAR(200),
    beverage        VARCHAR(200),
    allergens       TEXT[],                     -- ["gluten", "lactose", "egg", ...]
    has_vegetarian  BOOLEAN DEFAULT FALSE,
    observation     TEXT,
    UNIQUE (daily_menu_id, meal_type)
);

-- Alergias cadastradas no perfil do aluno (para alertas automáticos)
CREATE TABLE student_allergy_profiles (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    student_id      UUID NOT NULL REFERENCES students(id) ON DELETE CASCADE,
    allergens       TEXT[] NOT NULL,
    notes           TEXT,
    updated_at      TIMESTAMPTZ DEFAULT NOW(),
    UNIQUE (student_id)
);

-- ============================================================
-- CALENDAR — CALENDÁRIO ESCOLAR
-- ============================================================
CREATE TABLE calendar_events (
    id                      UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    title                   VARCHAR(200) NOT NULL,
    event_type              VARCHAR(30) NOT NULL,   -- meeting | holiday | field_trip | party | makeup_class | recess | general
    scope                   VARCHAR(20) NOT NULL,   -- school | classroom | student
    scope_entity_id         UUID,                   -- classroom_id ou student_id (null = escola toda)
    start_date              DATE NOT NULL,
    end_date                DATE,                   -- null = evento de 1 dia
    start_time              TIME,                   -- null = dia inteiro
    location                VARCHAR(300),
    description             TEXT,
    requires_confirmation   BOOLEAN DEFAULT FALSE,
    confirmation_deadline   DATE,
    reminder_days_before    INT[],                  -- [1, 3, 7] = lembrar 1, 3 e 7 dias antes
    status                  VARCHAR(20) NOT NULL DEFAULT 'draft',  -- draft | published | cancelled
    created_by              UUID NOT NULL,
    published_at            TIMESTAMPTZ,
    created_at              TIMESTAMPTZ DEFAULT NOW(),
    updated_at              TIMESTAMPTZ DEFAULT NOW()
);

CREATE TABLE event_attachments (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    event_id        UUID NOT NULL REFERENCES calendar_events(id) ON DELETE CASCADE,
    file_name       VARCHAR(200) NOT NULL,
    storage_key     TEXT NOT NULL,
    size_bytes      BIGINT,
    uploaded_at     TIMESTAMPTZ DEFAULT NOW()
);

CREATE TABLE event_attendance_confirmations (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    event_id        UUID NOT NULL REFERENCES calendar_events(id) ON DELETE CASCADE,
    parent_user_id  UUID NOT NULL,
    attending       BOOLEAN NOT NULL,
    note            TEXT,
    confirmed_at    TIMESTAMPTZ DEFAULT NOW(),
    updated_at      TIMESTAMPTZ DEFAULT NOW(),
    UNIQUE (event_id, parent_user_id)
);

-- ============================================================
-- INDEXES
-- ============================================================
CREATE INDEX idx_timelines_student_date   ON timelines(student_id, date DESC);
CREATE INDEX idx_timeline_entries_tl      ON timeline_entries(timeline_id);
CREATE INDEX idx_messages_channel         ON messages(channel_type, channel_id, sent_at DESC);
CREATE INDEX idx_invoices_student_status  ON invoices(student_id, status);

-- Nutrition
CREATE INDEX idx_menu_plans_week          ON menu_plans(school_year, week_start);
CREATE INDEX idx_daily_menus_plan         ON daily_menus(menu_plan_id, date);
CREATE INDEX idx_meal_entries_daily       ON meal_entries(daily_menu_id, meal_type);

-- Calendar
CREATE INDEX idx_calendar_events_date     ON calendar_events(start_date, end_date);
CREATE INDEX idx_calendar_events_scope    ON calendar_events(scope, scope_entity_id, start_date);
CREATE INDEX idx_calendar_events_status   ON calendar_events(status, start_date);
CREATE INDEX idx_attendance_conf_event    ON event_attendance_confirmations(event_id);
```

---

## 10. Stack Tecnológica

### Backend
| Tecnologia | Uso |
|---|---|
| **.NET 10 / C# 14** | Runtime principal |
| **Minimal APIs** | Endpoints REST |
| **MediatR** | CQRS + Domain Events |
| **FluentValidation** | Validação de commands |
| **EF Core 9 + Npgsql** | ORM + PostgreSQL |
| **Dapper** | Queries de leitura otimizadas |
| **Redis** | Cache distribuído + SignalR backplane |
| **RabbitMQ** | Event bus (domain → integration events) |
| **SignalR** | Notificações real-time |
| **Firebase Admin SDK** | Push notifications (iOS/Android) |
| **OpenTelemetry .NET** | Traces, metrics, logs |
| **Serilog** | Log estruturado |
| **Scrutor** | DI registration por convenção |
| **Polly** | Resiliência (retry, circuit breaker) |

### Frontend Web (Admin / Professor)
| Tecnologia | Uso |
|---|---|
| **Angular 19** | Framework principal |
| **Standalone Components** | Sem NgModule |
| **Signals + RxJS** | State management reativo |
| **Angular Material 3** | UI components (Material Design 3) |
| **Angular CDK** | Drag & drop, overlay, virtual scroll |
| **@microsoft/signalr** | Real-time (mensagens, timeline) |
| **NGX-Charts** | Gráficos e dashboards |
| **FullCalendar (Angular)** | Widget de calendário interativo |
| **Angular PWA** | Offline-first para professores |
| **Karma + Jasmine** | Testes unitários |
| **Playwright** | Testes E2E |

### Mobile (Pais / Professores em campo)
| Tecnologia | Uso |
|---|---|
| **Flutter 3.x (Dart 3)** | iOS + Android — single codebase |
| **flutter_local_notifications** | Notificações locais |
| **firebase_messaging** | Push notifications (FCM) |
| **dio + retrofit** | HTTP client tipado |
| **flutter_bloc (Cubit)** | State management |
| **go_router** | Navegação declarativa |
| **hive / isar** | Persistência local offline-first |
| **cached_network_image** | Cache de imagens |
| **flutter_secure_storage** | Armazenamento seguro (JWT) |
| **signalr_netcore** | SignalR client (real-time) |
| **flutter_calendar_carousel** | Widget de calendário |
| **fl_chart** | Gráficos (relatórios) |
| **Fastlane** | Build e distribuição (iOS/Android) |

### Infraestrutura
| Tecnologia | Uso |
|---|---|
| **PostgreSQL 16** | Banco principal (multi-schema) |
| **Redis 7** | Cache + pub/sub |
| **RabbitMQ 3.13** | Message broker |
| **MinIO** | Armazenamento de fotos/PDFs (S3-compatible) |
| **NGINX** | Reverse proxy + subdomain routing |
| **Docker + Compose** | Desenvolvimento local |
| **Kubernetes** | Produção (AKS / EKS) |
| **GitHub Actions** | CI/CD |

### Observabilidade
| Tecnologia | Uso |
|---|---|
| **OpenTelemetry Collector** | Coleta centralizada |
| **Grafana** | Dashboards |
| **Prometheus** | Métricas |
| **Jaeger / Tempo** | Distributed tracing |
| **Loki** | Log aggregation |

---

## 11. Segurança e LGPD

### Autenticação e Autorização
- **JWT + Refresh Token** com claims de tenant_id e user_type
- **Roles:** Parent | Teacher | Coordinator | SchoolAdmin | PlatformAdmin
- **Policy-based Authorization** com tenant isolation obrigatório

### LGPD
- **Dados de crianças:** requerem consentimento explícito dos responsáveis
- **DPO (Data Protection Officer):** campo obrigatório no cadastro da escola
- **Direito ao esquecimento:** endpoint de exclusão completa de dados do aluno
- **Portabilidade:** exportação de todos os dados em JSON/PDF
- **Minimização:** coletar apenas dados necessários por funcionalidade
- **Fotos:** privadas por padrão, visíveis apenas para pais do aluno
- **Logs de auditoria:** todas as ações sobre dados de menores são logadas

### Segurança Técnica
- TLS 1.3 obrigatório
- Secrets via HashiCorp Vault ou Azure Key Vault
- Rate limiting por tenant e por usuário
- Upload de fotos: validação MIME type + tamanho + antivírus scan
- SQL Injection: parametrizado via EF Core / Dapper
- XSS: sanitização de HTML em observações

---

## 12. Observabilidade

### Telemetria Customizada

```csharp
// Métricas de negócio
vikta.timeline.entries.created       (counter) — por tenant, por tipo
vikta.timeline.published.duration    (histogram) — tempo médio de preenchimento
vikta.notifications.push.sent        (counter) — por canal
vikta.notifications.push.delivered   (counter)
vikta.messages.sent                  (counter) — por canal
vikta.photos.uploaded.bytes          (histogram)

// Cardápio
vikta.menu_plan.published            (counter) — planos publicados/semana
vikta.allergen.alerts.triggered      (counter) — alertas disparados por aluno
vikta.menu_plan.update_after_publish (counter) — alterações pós-publicação

// Calendário
vikta.calendar_event.published       (counter) — por tipo de evento
vikta.attendance.confirmed           (counter) — confirmações de presença
vikta.ical.exports                   (counter) — exportações para Google/Apple

// SLIs
vikta.api.timeline.latency_p99       < 500ms
vikta.push.delivery.rate             > 99%
vikta.db.query.p95                   < 100ms
```

### Dashboards Grafana
- **Visão Operacional:** latência, error rate, throughput por endpoint
- **Visão de Negócio:** agendas preenchidas/dia, mensagens enviadas, alunos ativos
- **Visão Multi-tenant:** uso por escola (churn, engagement)

---

## 13. MVP — Escopo e Priorização

### 🏃 Sprint 1–2 (4 semanas): Fundação
- [ ] Setup do projeto (.NET 10, Minimal APIs, EF Core, PostgreSQL)
- [ ] Multi-tenancy middleware (schema per tenant)
- [ ] Identity Module: cadastro de escola, professor, pais
- [ ] JWT com claims de tenant e role
- [ ] CI/CD pipeline básico (GitHub Actions → Docker → VPS)

### 🏃 Sprint 3–4 (4 semanas): Agenda Digital (core)
- [ ] Timeline Aggregate Root completo
- [ ] Endpoints: criar/publicar agenda do dia
- [ ] App Flutter: tela de agenda para professores (mobile-first)
- [ ] App Flutter: feed de timeline para pais
- [ ] Upload de fotos (S3/MinIO)
- [ ] Push notification via Firebase quando agenda é publicada

### 🏃 Sprint 5–6 (4 semanas): Comunicação + Cardápio
- [ ] Módulo de mensagens (chat por turma)
- [ ] Confirmação de leitura
- [ ] Mural de comunicados da escola
- [ ] SignalR para mensagens em tempo real
- [ ] **Módulo Cardápio:** cadastro semanal pela coordenação
- [ ] **Módulo Cardápio:** visualização no app dos pais (home)
- [ ] **Módulo Cardápio:** alerta de alergênicos por aluno
- [ ] **Módulo Cardápio:** push toda sexta com cardápio da próxima semana

### 🏃 Sprint 7–8 (4 semanas): Calendário + Portal Web + Lançamento
- [ ] **Módulo Calendário:** CRUD de eventos com tipos e escopo
- [ ] **Módulo Calendário:** confirmação de presença (pais)
- [ ] **Módulo Calendário:** lembretes automáticos configuráveis
- [ ] **Módulo Calendário:** exportação iCal (Google/Apple Calendar)
- [ ] **Módulo Calendário:** upload de PDF de autorização (passeios)
- [ ] Galeria de fotos organizada
- [ ] Web Portal completo para professores e coordenação
- [ ] Notificações configuráveis (pais definem o que receber)
- [ ] Onboarding de escolas (self-service)
- [ ] Testes de carga + otimização
- [ ] Lançamento beta (1–3 escolas parceiras)

### 🔜 Pós-MVP
- Módulo financeiro (boleto, PIX)
- Módulo acadêmico (notas, frequência)
- Relatórios para médico (histórico de sono/alimentação + cardápio)
- Integração API de feriados nacionais (calendário automático)
- Cardápio com informações nutricionais detalhadas (plano Pro)
- Integração com ERPs escolares existentes

---

## 14. Roadmap

```
2026 Q1 — Fundação + Agenda Digital (MVP Core)
│
│  Jan-Fev: Setup arquitetura, multi-tenancy, identity
│  Mar-Abr: Agenda digital completa, push notifications
│  Mai:     Piloto com 2–3 escolas (beta fechado)
│
2026 Q2 — Comunicação + Portal Web
│
│  Jun-Jul: Chat por turma, mural, galeria
│  Ago:     Web portal completo para escola
│  Set:     Beta aberto (10–20 escolas)
│
2026 Q3 — Financeiro + Crescimento
│
│  Out-Nov: Módulo financeiro (boleto/PIX)
│  Dez:     Lançamento oficial, SLA, suporte
│
2027 Q1 — Acadêmico + Integrações
│
│  Jan-Mar: Notas, frequência, relatórios
│           Integração com ERPs (Totvs, Escola Web)
│           API pública para parceiros
```

---

## 15. Modelo de Negócio

### Pricing (SaaS B2B)

| Plano | Alunos | Preço/mês | Recursos |
|---|---|---|---|
| **Starter** | até 50 | R$ 199 | Agenda + Chat + Galeria |
| **Growth** | até 200 | R$ 499 | + Financeiro + Calendário + Relatórios |
| **Pro** | até 500 | R$ 999 | + API + White-label + Suporte dedicado |
| **Enterprise** | ilimitado | Sob consulta | + Integrações customizadas + SLA 99,9% |

### Unit Economics (referência)
- **CAC:** R$ 400–800 (via indicação de pais + LinkedIn para diretores)
- **LTV médio:** 24 meses × R$ 499 = R$ 11.976
- **LTV/CAC:** ~15–20x (saudável)
- **Churn esperado:** < 5% a.m. (escolas têm baixa rotatividade)

### Estratégia de Aquisição
1. **Bottom-up:** Pais que amam o produto indicam a escola
2. **Outbound:** Abordagem direta em associações de escolas
3. **Parceria:** Distribuidores regionais de materiais escolares

---

## 16. Estrutura do Projeto (.NET)

```
Vikta/
├── src/
│   ├── Vikta.Domain/
│   │   ├── Communication/
│   │   │   ├── Aggregates/
│   │   │   │   ├── Timeline/
│   │   │   │   │   ├── Timeline.cs
│   │   │   │   │   ├── TimelineEntry.cs
│   │   │   │   │   ├── TimelineId.cs
│   │   │   │   │   └── TimelineStatus.cs
│   │   │   │   ├── Message/
│   │   │   │   │   ├── Message.cs
│   │   │   │   │   └── MessageReadReceipt.cs
│   │   │   │   └── Announcement/
│   │   │   │       └── Announcement.cs
│   │   │   ├── ValueObjects/
│   │   │   │   ├── Photo.cs
│   │   │   │   ├── MealQuality.cs
│   │   │   │   └── EntryPayload.cs
│   │   │   └── DomainEvents/
│   │   │       ├── TimelineCreatedEvent.cs
│   │   │       ├── TimelineEntryAddedEvent.cs
│   │   │       └── TimelinePublishedEvent.cs
│   │   │
│   │   ├── Academic/
│   │   │   ├── Aggregates/
│   │   │   │   ├── Student/
│   │   │   │   │   ├── Student.cs
│   │   │   │   │   └── ParentLink.cs
│   │   │   │   └── Classroom/
│   │   │   │       └── Classroom.cs
│   │   │   └── DomainEvents/
│   │   │
│   │   ├── Financial/
│   │   │   └── Aggregates/
│   │   │       └── Invoice/
│   │   │           ├── Invoice.cs
│   │   │           └── Payment.cs
│   │   │
│   │   ├── Identity/
│   │   │   └── Aggregates/
│   │   │       └── User/
│   │   │           ├── User.cs
│   │   │           └── UserType.cs
│   │   │
│   │   └── Shared/
│   │       ├── AggregateRoot.cs
│   │       ├── Entity.cs
│   │       ├── ValueObject.cs
│   │       ├── IDomainEvent.cs
│   │       ├── DomainException.cs
│   │       └── Result.cs
│   │
│   ├── Vikta.Application/
│   │   ├── UseCases/
│   │   │   ├── Communication/
│   │   │   │   ├── Commands/
│   │   │   │   │   ├── PublishTimeline/
│   │   │   │   │   │   ├── PublishTimelineCommand.cs
│   │   │   │   │   │   └── PublishTimelineCommandHandler.cs
│   │   │   │   │   └── AddTimelineEntry/
│   │   │   │   │       ├── AddTimelineEntryCommand.cs
│   │   │   │   │       └── AddTimelineEntryCommandHandler.cs
│   │   │   │   └── Queries/
│   │   │   │       └── GetStudentTimeline/
│   │   │   │           ├── GetStudentTimelineQuery.cs
│   │   │   │           └── GetStudentTimelineQueryHandler.cs
│   │   │   ├── Academic/
│   │   │   └── Financial/
│   │   │
│   │   ├── DTOs/
│   │   │   ├── TimelineDto.cs
│   │   │   └── TimelineEntryDto.cs
│   │   │
│   │   ├── Behaviors/
│   │   │   ├── ValidationBehavior.cs
│   │   │   ├── TenantIsolationBehavior.cs
│   │   │   ├── LoggingBehavior.cs
│   │   │   └── PerformanceBehavior.cs
│   │   │
│   │   ├── DomainEventHandlers/
│   │   │   └── TimelinePublishedEventHandler.cs
│   │   │
│   │   └── Interfaces/
│   │       ├── ITimelineRepository.cs
│   │       ├── IStudentRepository.cs
│   │       ├── IPhotoStorageService.cs
│   │       ├── INotificationService.cs
│   │       └── IUnitOfWork.cs
│   │
│   ├── Vikta.Infrastructure/
│   │   ├── Persistence/
│   │   │   ├── ViktaDbContext.cs
│   │   │   ├── Configurations/
│   │   │   │   ├── TimelineConfiguration.cs
│   │   │   │   └── StudentConfiguration.cs
│   │   │   ├── Repositories/
│   │   │   │   ├── TimelineRepository.cs
│   │   │   │   └── StudentRepository.cs
│   │   │   └── Migrations/
│   │   │
│   │   ├── MultiTenancy/
│   │   │   ├── TenantMiddleware.cs
│   │   │   ├── TenantResolver.cs
│   │   │   └── TenantContext.cs
│   │   │
│   │   ├── Identity/
│   │   │   └── JwtTokenService.cs
│   │   │
│   │   ├── Payments/
│   │   │   ├── PIXProvider.cs
│   │   │   └── BoletoProvider.cs
│   │   │
│   │   ├── Storage/
│   │   │   └── MinIOPhotoStorage.cs
│   │   │
│   │   ├── Messaging/
│   │   │   └── RabbitMQEventBus.cs
│   │   │
│   │   ├── Notifications/
│   │   │   └── FirebasePushNotificationService.cs
│   │   │
│   │   └── Observability/
│   │       └── OpenTelemetryConfiguration.cs
│   │
│   └── Vikta.Api/
│       ├── Program.cs
│       ├── Endpoints/
│       │   ├── Communication/
│       │   │   ├── TimelineEndpoints.cs
│       │   │   └── MessageEndpoints.cs
│       │   ├── Academic/
│       │   │   └── StudentEndpoints.cs
│       │   └── Financial/
│       │       └── InvoiceEndpoints.cs
│       ├── Middleware/
│       │   └── ErrorHandlingMiddleware.cs
│       └── appsettings.json
│
├── tests/
│   ├── Vikta.Domain.Tests/
│   │   └── Communication/
│   │       └── TimelineTests.cs
│   ├── Vikta.Application.Tests/
│   │   └── UseCases/
│   │       └── AddTimelineEntryCommandHandlerTests.cs
│   ├── Vikta.Infrastructure.Tests/
│   │   └── Repositories/
│   │       └── TimelineRepositoryTests.cs
│   └── Vikta.Api.Tests/
│       └── Endpoints/
│           └── TimelineEndpointsTests.cs
│
├── docker-compose.yml
├── docker-compose.override.yml
└── README.md
```

### Estrutura do App Flutter (vikta-app)

```
vikta-app/
├── lib/
│   ├── main.dart
│   ├── app/
│   │   ├── app.dart                    # MaterialApp + GoRouter setup
│   │   └── router/
│   │       └── app_router.dart         # Rotas declarativas
│   │
│   ├── core/
│   │   ├── api/
│   │   │   ├── vikta_api_client.dart   # Dio + interceptors
│   │   │   └── auth_interceptor.dart   # JWT attach + refresh
│   │   ├── storage/
│   │   │   ├── secure_storage.dart     # flutter_secure_storage (JWT)
│   │   │   └── local_db.dart           # Isar (cache offline)
│   │   ├── notifications/
│   │   │   └── push_notification_service.dart  # FCM
│   │   └── signalr/
│   │       └── realtime_service.dart   # SignalR client
│   │
│   ├── features/
│   │   ├── auth/
│   │   │   ├── cubit/auth_cubit.dart
│   │   │   └── view/login_page.dart
│   │   │
│   │   ├── timeline/                   # Agenda Digital
│   │   │   ├── cubit/timeline_cubit.dart
│   │   │   ├── data/
│   │   │   │   ├── timeline_repository.dart
│   │   │   │   └── timeline_api.dart
│   │   │   └── view/
│   │   │       ├── timeline_page.dart      # Feed do pai
│   │   │       ├── timeline_form_page.dart # Formulário da professora
│   │   │       └── widgets/
│   │   │           ├── meal_card.dart
│   │   │           ├── nap_card.dart
│   │   │           └── activity_card.dart
│   │   │
│   │   ├── menu/                       # Cardápio
│   │   │   ├── cubit/menu_cubit.dart
│   │   │   ├── data/menu_repository.dart
│   │   │   └── view/
│   │   │       ├── menu_week_page.dart
│   │   │       └── widgets/
│   │   │           ├── meal_tile.dart
│   │   │           └── allergen_badge.dart
│   │   │
│   │   ├── calendar/                   # Calendário
│   │   │   ├── cubit/calendar_cubit.dart
│   │   │   ├── data/calendar_repository.dart
│   │   │   └── view/
│   │   │       ├── calendar_page.dart
│   │   │       ├── event_detail_page.dart
│   │   │       └── widgets/
│   │   │           └── event_chip.dart
│   │   │
│   │   ├── messages/                   # Chat + Mural
│   │   │   ├── cubit/messages_cubit.dart
│   │   │   └── view/
│   │   │       ├── classroom_chat_page.dart
│   │   │       └── mural_page.dart
│   │   │
│   │   └── gallery/
│   │       └── view/gallery_page.dart
│   │
│   └── shared/
│       ├── theme/vikta_theme.dart      # Design system
│       ├── widgets/
│       │   ├── vikta_app_bar.dart
│       │   └── loading_indicator.dart
│       └── extensions/
│           └── date_extensions.dart
│
├── test/
│   ├── features/timeline/timeline_cubit_test.dart
│   └── features/menu/menu_cubit_test.dart
│
└── pubspec.yaml
```

### Estrutura do Portal Web Angular (vikta-web)

```
vikta-web/
├── src/
│   ├── app/
│   │   ├── app.config.ts               # Standalone app config
│   │   ├── app.routes.ts               # Roteamento principal
│   │   │
│   │   ├── core/
│   │   │   ├── auth/
│   │   │   │   ├── auth.service.ts
│   │   │   │   └── auth.guard.ts
│   │   │   ├── api/
│   │   │   │   └── vikta-http.service.ts
│   │   │   └── signalr/
│   │   │       └── realtime.service.ts
│   │   │
│   │   ├── features/
│   │   │   ├── dashboard/
│   │   │   │   └── dashboard.component.ts
│   │   │   │
│   │   │   ├── timeline/               # Agenda digital (professor)
│   │   │   │   ├── timeline.routes.ts
│   │   │   │   ├── timeline-list/
│   │   │   │   └── timeline-form/
│   │   │   │       └── timeline-form.component.ts
│   │   │   │
│   │   │   ├── menu/                   # Cardápio (coordenação)
│   │   │   │   ├── menu-week/
│   │   │   │   │   └── menu-week.component.ts
│   │   │   │   └── menu-form/
│   │   │   │       └── menu-form.component.ts
│   │   │   │
│   │   │   ├── calendar/               # Calendário (coordenação)
│   │   │   │   ├── calendar.component.ts  # FullCalendar integration
│   │   │   │   └── event-dialog/
│   │   │   │       └── event-dialog.component.ts
│   │   │   │
│   │   │   ├── students/
│   │   │   │   └── student-list/
│   │   │   │
│   │   │   └── reports/
│   │   │       └── reports.component.ts
│   │   │
│   │   └── shared/
│   │       ├── components/
│   │       │   └── vikta-toolbar/
│   │       └── pipes/
│   │           └── allergen.pipe.ts
│   │
│   ├── environments/
│   │   ├── environment.ts
│   │   └── environment.prod.ts
│   └── styles.scss
│
├── angular.json
└── package.json
```

---

## 17. Infraestrutura e DevOps

### Docker Compose (desenvolvimento local)

```yaml
services:
  vikta-api:
    build: ./src/Vikta.Api
    ports: ["5000:8080"]
    environment:
      ConnectionStrings__Default: "Host=postgres;Database=vikta;Username=vikta;Password=vikta123"
      Redis__ConnectionString: "redis:6379"
      RabbitMQ__Host: "rabbitmq"
    depends_on: [postgres, redis, rabbitmq, minio]

  postgres:
    image: postgres:16-alpine
    environment:
      POSTGRES_DB: vikta
      POSTGRES_USER: vikta
      POSTGRES_PASSWORD: vikta123
    volumes: ["postgres_data:/var/lib/postgresql/data"]
    ports: ["5432:5432"]

  redis:
    image: redis:7-alpine
    ports: ["6379:6379"]

  rabbitmq:
    image: rabbitmq:3.13-management
    ports: ["5672:5672", "15672:15672"]

  minio:
    image: minio/minio
    command: server /data --console-address ":9001"
    environment:
      MINIO_ROOT_USER: vikta
      MINIO_ROOT_PASSWORD: vikta123
    ports: ["9000:9000", "9001:9001"]
    volumes: ["minio_data:/data"]

  # Observabilidade
  otel-collector:
    image: otel/opentelemetry-collector-contrib
    volumes: ["./otel-config.yml:/etc/otelcol-contrib/config.yaml"]

  grafana:
    image: grafana/grafana
    ports: ["3000:3000"]

  prometheus:
    image: prom/prometheus
    ports: ["9090:9090"]

  jaeger:
    image: jaegertracing/all-in-one
    ports: ["16686:16686"]

volumes:
  postgres_data:
  minio_data:
```

### CI/CD Pipeline (GitHub Actions)

```yaml
# .github/workflows/ci.yml
name: Vikta CI/CD

on:
  push:
    branches: [main, develop]
  pull_request:
    branches: [main]

jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with: { dotnet-version: '10.x' }
      - run: dotnet test --collect:"XPlat Code Coverage"
      - uses: codecov/codecov-action@v4

  test-flutter:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: subosito/flutter-action@v2
        with: { flutter-version: '3.x', channel: 'stable' }
      - run: cd vikta-app && flutter pub get
      - run: cd vikta-app && flutter test
      - run: cd vikta-app && flutter analyze

  test-angular:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-node@v4
        with: { node-version: '22' }
      - run: cd vikta-web && npm ci
      - run: cd vikta-web && npm test -- --watch=false
      - run: cd vikta-web && npx playwright test

  build-and-push:
    needs: test
    if: github.ref == 'refs/heads/main'
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - name: Build Docker image
        run: docker build -t vikta/api:${{ github.sha }} .
      - name: Push to registry
        run: docker push vikta/api:${{ github.sha }}

  deploy:
    needs: build-and-push
    runs-on: ubuntu-latest
    steps:
      - name: Deploy to Kubernetes
        run: kubectl set image deployment/vikta-api api=vikta/api:${{ github.sha }}
```

---

## 18. Próximos Passos

### Imediatos (próximas 2 semanas)

- [ ] Criar repositório GitHub `vikta-backend`
- [ ] Setup do projeto .NET 10 com estrutura modular
- [ ] Configurar Docker Compose com PostgreSQL, Redis, RabbitMQ
- [ ] Implementar multi-tenancy middleware
- [ ] Implementar Identity Module (cadastro + JWT)
- [ ] Criar primeiro Aggregate: Timeline
- [ ] Primeiro endpoint: `POST /api/timeline/{studentId}/entries`
- [ ] Criar repositório `vikta-app` (Flutter)
- [ ] Setup Flutter: estrutura de pastas + flutter_bloc + go_router
- [ ] Criar repositório `vikta-web` (Angular 19)

### Validação de Mercado (paralelo)

- [ ] Conversar com 5–10 pais de escolas infantis (validar dores)
- [ ] Apresentar conceito para 2–3 diretores de escola
- [ ] Criar landing page (vikta.com.br) com waitlist
- [ ] Definir escola parceira para piloto beta

### Decisões Técnicas a Confirmar

- [ ] Firebase vs OneSignal (push notifications)
- [ ] Azure vs AWS vs VPS nacional (hospedagem)
- [ ] PIX: Gerencianet vs Pagar.me vs Iugu

---

*Documento gerado em: 12/02/2026*
*Versão: 1.2.0 — Stack definitiva: .NET 10 + Minimal API · Angular 19 · Flutter 3.x*
*Projeto: Vikta — Comunicação Escola-Família*
